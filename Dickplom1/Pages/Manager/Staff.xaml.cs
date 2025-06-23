using CustomControlsForDiplomFramework;
using Dickplom1.Class;
using Dickplom1.DataFolder;
using Dickplom1.Windows.Others;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace Dickplom1.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для Staff.xaml
    /// </summary>
    public partial class Staff : Page
    {
        public Staff()
        {
            InitializeComponent();

            foreach (Window w in Application.Current.Windows)
            {
                if (w is MainWindow mainWnd)
                {
                    mainWin = mainWnd;
                    break;
                }
            }

            if (mainWin == null)
            {
                return;
            }
        }
        public MainWindow mainWin = null;
        public bool IsDeletedFilter { get; set; } = false;

        //Загрузка данных в датагрид и паггинация
        public List<StaffViewModel> allStaff;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalPages = 1;

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshItems();
        }

        public void SetPaggination()
        {
            CheckTotalPages();
            LoadCurrentPage();
            GeneratePaginationButtons();
        }

        public void RefreshItems()
        {
            var context = DBEntities.GetContext();

            if (!IsDeletedFilter)
            {
                allStaff = context.Users
                .Where(o => o.IsDeleted == false && o.UserData != null)
                .Select
                (o => new StaffViewModel
                {
                    UserId = o.UserId,
                    UserDataId = o.UserData.UserDataId,
                    UserPhoto = o.UserData.UserPhoto,
                    FIOStaff = o.UserData.Surname + " " + o.UserData.Name + " " + o.UserData.MiddleName,
                    Email = o.UserData.Email,
                    Login = o.Login,
                    PhoneNumber = o.UserData.PhoneNumber ?? " ",
                    Role = o.Roles.NameRole ?? " ",
                    AccountStatusId = o.AccountStatusId ?? 0,
                    AccountStatus = o.AccountStatus.AccountStatusValue ?? " ",
                    IsDeleted = o.IsDeleted,
                    CreatorId = o.CreatorId ?? 2,
                    CreatedAt = o.CreatedAt ?? DateTime.MinValue
                })
                .ToList();
            }
            else
            {
                allStaff = context.Users
                .Where(o => o.IsDeleted == true && o.UserData != null)
                .Select
                (o => new StaffViewModel
                {
                    UserId = o.UserId,
                    UserDataId = o.UserData.UserDataId,
                    UserPhoto = o.UserData.UserPhoto,
                    FIOStaff = o.UserData.Surname + " " + o.UserData.Name + " " + o.UserData.MiddleName,
                    Email = o.UserData.Email,
                    Login = o.Login,
                    PhoneNumber = o.UserData.PhoneNumber ?? " ",
                    Role = o.Roles.NameRole ?? " ",
                    AccountStatusId = o.AccountStatusId ?? 0,
                    AccountStatus = o.AccountStatus.AccountStatusValue ?? " ",
                    IsDeleted = o.IsDeleted,
                    CreatorId = o.CreatorId ?? 2,
                    CreatedAt = o.CreatedAt ?? DateTime.MinValue
                })
                .ToList();
            }

            currentPage = 1;
            SetPaggination();
        }
        private void CheckTotalPages()
        {
            totalPages = (int)Math.Ceiling((double)allStaff.Count / 10);
        }
        private void GeneratePaginationButtons()
        {
            spPaggination.Children.Clear();

            void AddButton(int pageNumber, bool isCurrent = false)
            {
                var btn = new PagginationButtons();
                btn.rbtnPag.Content = pageNumber.ToString();
                btn.rbtnPag.Tag = pageNumber;
                btn.rbtnPag.IsChecked = isCurrent;
                btn.rbtnPag.Click += RbtnPag_Click;
                spPaggination.Children.Add(btn);
            }

            void AddEllipsis()
            {
                var textBlock = new TextBlock
                {
                    Text = "...",
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5, 0, 5, 0)
                };
                spPaggination.Children.Add(textBlock);
            }

            const int maxVisibleButtons = 15;

            if (totalPages <= maxVisibleButtons)
            {
                // Показываем все страницы
                for (int i = 1; i <= totalPages; i++)
                {
                    AddButton(i, i == currentPage);
                }
            }
            else
            {
                AddButton(1, currentPage == 1);

                // Левая сторона
                if (currentPage > 4)
                    AddEllipsis();

                int start = Math.Max(2, currentPage - 2);
                int end = Math.Min(totalPages - 1, currentPage + 2);

                for (int i = start; i <= end; i++)
                {
                    AddButton(i, i == currentPage);
                }

                // Правая сторона
                if (currentPage < totalPages - 3)
                    AddEllipsis();

                AddButton(totalPages, currentPage == totalPages);
            }
        }

        private void RbtnPag_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rbtn && int.TryParse(rbtn.Tag.ToString(), out int page))
            {
                currentPage = page;
                LoadCurrentPage();
                GeneratePaginationButtons();
            }
        }
        private void LoadCurrentPage()
        {
            var itemsToShow = allStaff
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select((c, index) => {
                    c.Number = (currentPage - 1) * itemsPerPage + index + 1;
                    return c;
                })
                .ToList();

            if (itemsToShow.Count <= 0)
                tbInfo.Visibility = Visibility.Visible;
            else
                tbInfo.Visibility = Visibility.Collapsed;

            dataGrid.dg.ItemsSource = itemsToShow;
        }

        private void ComboboxesFilter_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка — объект с FullName и UserDataId = 0 или null
            items.Add(new { AccountStatusId = 0, AccountStatusValue = "Статус учетной записи" });

            items.AddRange(context.AccountStatus
                .Select(u => new
                {
                    AccountStatusId = u.AccountStatusId,
                    AccountStatusValue = u.AccountStatusValue
                }));

            ComboboxesFilter.firstCombobox.ItemsSource = items;
            ComboboxesFilter.firstCombobox.DisplayMemberPath = "AccountStatusValue";
            ComboboxesFilter.firstCombobox.SelectedValuePath = "AccountStatusId";
            ComboboxesFilter.firstCombobox.SelectedIndex = 0;
            ComboboxesFilter.firstCombobox.SelectionChanged += FirstCombobox_SelectionChanged;
            ComboboxesFilter.gridFilter.Height = 150;


            //Добавить 2-ой комбобокс
            ComboboxMaterialDesignWithBorder cbox = new ComboboxMaterialDesignWithBorder();

            var items2 = new List<object>();
            items2.Add(new { RoleId = 0, NameRole = "Должность" });

            items2.AddRange(context.Roles
                .Select(u => new
                {
                    RoleId = u.RoleId,
                    NameRole = u.NameRole,
                }));

            cbox.cbox.ItemsSource = items2;
            cbox.Name = "cbox";
            cbox.cbox.DisplayMemberPath = "NameRole";
            cbox.cbox.SelectedValuePath = "RoleId";
            cbox.cbox.SelectedIndex = 0;
            cbox.cbox.SelectionChanged += Cbox_SelectionChanged;
            cbox.Margin = new Thickness(15, 0, 15, 0);

            ComboboxesFilter.spCboxes.Children.Add(cbox);
        }
        public int cboxAccountStatusId { get; set; } = 0;
        public int cboxRoleId { get; set; } = 0;

        private void FirstCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboxAccountStatusId = Convert.ToInt32(ComboboxesFilter.firstCombobox.SelectedValue);
            ApplyFilters();
        }
        private void Cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.ComboBox cbox)
            {
                cboxRoleId = Convert.ToInt32(cbox.SelectedValue);
                ApplyFilters();
            }
        }

        public void ApplyFilters()
        {
            var context = DBEntities.GetContext();

            if (!IsDeletedFilter)
            {
                var staffQuery = context.Users
                    .Where(c => !c.IsDeleted);

                // фильтр по статусу учетной записи
                if (cboxAccountStatusId != 0)
                {
                    staffQuery = staffQuery.Where(c => c.AccountStatusId == cboxAccountStatusId);
                }

                // фильтр по должности
                if (cboxRoleId != 0)
                {
                    staffQuery = staffQuery.Where(c => c.RoleId == cboxRoleId);
                }

                var filteredUsers = staffQuery
                    .Where(x => !x.IsDeleted)
                    .ToList() // Загружаем данные в память
                    .Select(c => new StaffViewModel
                    {
                        UserId = c.UserId,
                        AccountStatusId = (int)c.AccountStatusId,
                        UserDataId = (int)c.UserDataId,
                        UserPasswordDataId = (int)c.UserPassportDataId,
                        RoleId = (int)c.RoleId,
                        Login = c.Login,
                        PasswordHash = c.PasswordHash,
                        CreatorId = c.CreatorId ?? 2,
                        CreatedAt = (DateTime)c.CreatedAt,
                        IsDeleted = c.IsDeleted,

                        FIOStaff = c.UserData?.Surname + " " + c.UserData?.Name + " " + c.UserData?.MiddleName + " ",
                        Email = c.UserData?.Email,
                        PhoneNumber = c.UserData?.PhoneNumber,
                        Role = context.Roles.FirstOrDefault(f=>f.RoleId == c.RoleId).NameRole ?? "",
                        AccountStatus = c.AccountStatus.AccountStatusValue,
                        UserPhoto = c.UserData.UserPhoto
                    })
                    .ToList();

                if (allStaff != null)
                    allStaff.Clear();
                allStaff = filteredUsers;
            }
            else // фильтр по удаленным
            {
                var staffQuery = context.Users
                    .Where(c => c.IsDeleted);


                // фильтр по статусу учетной записи
                if (cboxAccountStatusId != 0)
                {
                    staffQuery = staffQuery.Where(c => c.AccountStatusId == cboxAccountStatusId);
                }

                // фильтр по должности
                if (cboxRoleId != 0)
                {
                    staffQuery = staffQuery.Where(c => c.RoleId == cboxRoleId);
                }

                var filteredUsers = staffQuery
                    .Where(x => x.IsDeleted)
                    .ToList() // Загружаем данные в память
                    .Select(c => new StaffViewModel
                    {
                        UserId = c.UserId,
                        AccountStatusId = (int)c.AccountStatusId,
                        UserDataId = (int)c.UserDataId,
                        UserPasswordDataId = (int)c.UserPassportDataId,
                        RoleId = (int)c.RoleId,
                        Login = c.Login,
                        PasswordHash = c.PasswordHash,
                        CreatorId = c.CreatorId ?? 2,
                        CreatedAt = (DateTime)c.CreatedAt,
                        IsDeleted = c.IsDeleted,

                        FIOStaff = c.UserData?.Surname + " " + c.UserData?.Name + " " + c.UserData?.MiddleName + " ",
                        Email = c.UserData?.Email,
                        PhoneNumber = c.UserData?.PhoneNumber,
                        Role = c.Roles.NameRole,
                        AccountStatus = c.AccountStatus.AccountStatusValue,
                        UserPhoto = c.UserData.UserPhoto
                    })
                    .ToList();

                if (allStaff != null)
                    allStaff.Clear();
                allStaff = filteredUsers;
            }

            CheckTotalPages();
            GeneratePaginationButtons();
            LoadCurrentPage();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (mainWin!= null && mainWin.gridSearch != null)
            {
                mainWin.gridSearch.Visibility = Visibility.Visible;

            }
            Dickplom1.Class.Musor.SearchSelect();
        }

        private void ButtomWithBorder_Loaded(object sender, RoutedEventArgs e)
        {
            btnAddStaff.btnWithBorder.Click += BtnWithBorder_Click;
        }

        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            MiniProfileForAdminWin win = new MiniProfileForAdminWin();
            win.Closed += Win_Closed1;
            win.ShowDialog();
        }

        private void Win_Closed1(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) // Удаление
        {
            var context = DBEntities.GetContext();
            if (dataGrid.dg.SelectedItem is StaffViewModel itemNew)
            {
                if (itemNew.UserId == mainWin.ActiveUser.UserId)
                {
                    MessageBox.Show("Вы не можете удалить свою учетную запись");
                    return;
                }
            }
            MessageBoxButton btns = MessageBoxButton.YesNo;
            MessageBoxResult box = MessageBox.Show("Вы уверенны?", "Внимание", btns);

            if (box == MessageBoxResult.Yes)
                if (dataGrid.dg.SelectedItem is StaffViewModel item)
                {
                    var selectedUser = context.Users.FirstOrDefault(c => c.UserDataId == item.UserDataId);

                    if (IsDeletedFilter)
                    {
                        try
                        {
                            if (selectedUser != null)
                            {
                                context.UserData.Remove(selectedUser.UserData);
                                context.UserPassportData.Remove(selectedUser.UserPassportData);
                                context.Users.Remove(selectedUser);

                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        try
                        {
                            if (selectedUser != null)
                            {
                                selectedUser.IsDeleted = true;
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    context.SaveChanges();
                    RefreshItems();
                    LoadCurrentPage();
                }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            if (dataGrid.dg.SelectedItem is StaffViewModel item)
            {
                if (mainWin.ActiveUser != null)
                {
                    if (mainWin.ActiveUser.RoleId == 1)
                    {
                        MiniProfileForAdminWin win = new MiniProfileForAdminWin();
                        win.SelectedUser = context.Users.FirstOrDefault(f => f.UserId == item.UserId) ?? null;
                        win.Closed += Win_Closed;
                        win.ShowDialog();
                    }
                    else if (mainWin.ActiveUser.RoleId == 2)
                    {
                        MiniProfileForStaff win = new MiniProfileForStaff();
                        win.SelectedUser = context.Users.FirstOrDefault(f=>f.UserId == item.UserId) ?? null;
                        win.Closed += Win_Closed;
                        win.ShowDialog();
                    }
                }
            }
        }
        private void Win_Closed(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();
            if (dataGrid.dg.SelectedItem is StaffViewModel item)
            {
                MessageBoxButton btns = MessageBoxButton.YesNo;
                MessageBoxResult box = MessageBox.Show("Вы уверенны?", "Внимание", btns);

                if (box == MessageBoxResult.Yes)
                {
                    var selectedUser = context.Users.FirstOrDefault(f=>f.UserDataId == item.UserDataId);
                    if (selectedUser != null)
                    {
                        ResetUserPassword(selectedUser);
                    }

                }
            }    
        }
        private void ResetUserPassword(Users selectedUser)
        {
            var context = DBEntities.GetContext();

            string tempPassword = Dickplom1.Class.PasswordHelper.GenerateTemporaryPassword();
            selectedUser.PasswordHash = Dickplom1.Class.PasswordHelper.HashPassword(tempPassword);
            selectedUser.IsTemporaryPassword = true;
            selectedUser.AccountStatusId = 2;

            context.SaveChanges();
            CustomPasswordWindow win = new CustomPasswordWindow();
            win.FullName = selectedUser.UserData?.Surname + " " + selectedUser.UserData?.Name + " " + selectedUser.UserData?.MiddleName;
            win.TempPassword = tempPassword;
            win.ShowDialog(); 
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e) // Заблокировать
        {
            var context = DBEntities.GetContext();
            if (dataGrid.dg.SelectedItem is StaffViewModel item)
            {
                if (item.AccountStatusId == 3)
                {
                    MessageBox.Show("Данный пользователь уже заблокирован");
                    return;
                }
                else if (context.Users.FirstOrDefault(f=>f.UserDataId == item.UserDataId).UserId == mainWin.ActiveUser.UserId)
                {
                    MessageBox.Show("Текущую учетную запись невозможно заблокировать");
                    return;
                }
                else if (item.AccountStatusId == 1 | item.AccountStatusId == 2)
                {
                    try
                    {
                        context.Users.FirstOrDefault(f => f.UserDataId == item.UserDataId).AccountStatusId = 3;
                        context.SaveChanges();
                        MessageBox.Show("Пользователь заблокирован");
                        RefreshItems();
                        return;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            StaffManagerMiniProfile win = new StaffManagerMiniProfile();

            if (dataGrid.dg.SelectedItem is StaffViewModel item)
            {
                var staff = DBEntities.GetContext().Users
                    .FirstOrDefault(u => u.UserDataId == item.CreatorId);
                if (staff != null)
                {
                    win.StaffId = staff.UserData.UserDataId;
                    win.ShowDialog();
                }
                else
                    MessageBox.Show("Создатель не найден");

            }
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e) // Разблокировать
        {
            var context = DBEntities.GetContext();

            try
            {
                if (dataGrid.dg.SelectedItem is StaffViewModel item)
                {
                    var staff = DBEntities.GetContext().Users.FirstOrDefault(u => u.UserDataId == item.UserDataId);
                    if (staff != null)
                    {
                        if (staff.AccountStatusId == 2 | staff.AccountStatusId == 1)
                        {
                            MessageBox.Show("Пользователь не заблокирован");
                            return;
                        }
                        if (staff.AccountStatusId == 3)
                        {
                            var selectedUser = context.Users.FirstOrDefault(f => f.UserDataId == item.UserDataId);
                            if (selectedUser != null)
                            {
                                if (selectedUser.IsTemporaryPassword == true)
                                {
                                    selectedUser.AccountStatusId = 2;
                                }
                                else if (selectedUser.IsTemporaryPassword == false)
                                {
                                    selectedUser.AccountStatusId = 1;
                                }
                                context.SaveChanges();
                                MessageBox.Show("Пользователь разблокирован");
                                RefreshItems();
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void dataGrid_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MenuItem miBtnRecovery = dataGrid.ContextMenu?.Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(mi => (string)mi.Header == "Восстановить");

                MenuItem miBtnBlock = dataGrid.ContextMenu?.Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(mi => (string)mi.Header == "Заблокировать");

                MenuItem miBtnOnBlock = dataGrid.ContextMenu?.Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(mi => (string)mi.Header == "Разблокировать");

                MenuItem miBtnDelete = dataGrid.ContextMenu?.Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(mi => (string)mi.Header == "Удалить");

                MenuItem miBtnResetPassword = dataGrid.ContextMenu?.Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(mi => (string)mi.Header == "Сбросить пароль");

                if (dataGrid.dg.SelectedItem is StaffViewModel item)
                {
                    if (miBtnOnBlock != null)
                    {
                        if (item.AccountStatusId != 3) // Разблокировать
                        {
                            miBtnOnBlock.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            miBtnOnBlock.Visibility = Visibility.Visible;
                        }
                    }


                    if (miBtnBlock != null)
                    {
                        if (item.AccountStatusId == 3) // Заблокировать
                        {
                            miBtnBlock.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            miBtnBlock.Visibility = Visibility.Visible;
                        }
                    }


                    if (miBtnRecovery != null)
                    {
                        if (!item.IsDeleted) // Восстановить
                        {
                            miBtnRecovery.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            miBtnRecovery.Visibility = Visibility.Visible;
                        }
                    }

                    if (mainWin.ActiveUser != null)
                    {
                        if (mainWin.ActiveUser.RoleId != 1)
                        {
                            miBtnRecovery.Visibility = Visibility.Collapsed;
                            miBtnBlock.Visibility = Visibility.Collapsed;
                            miBtnOnBlock.Visibility = Visibility.Collapsed;
                            miBtnDelete.Visibility = Visibility.Collapsed;
                            miBtnResetPassword.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void DeletedRecords_Loaded(object sender, RoutedEventArgs e)
        {
            spDeletedRecords.stackPanel.MouseLeftButtonUp += StackPanel_MouseLeftButtonUp;
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDeletedFilter = true; //активируем флажок

            Dickplom1.Class.Musor.ShowElement(spBack); // Включаем кнопку вернуть
            Dickplom1.Class.Musor.HideElement(spDeletedRecords); // Выключаем кнопку удаленных записей

            try
            {
                MenuItem miBtn = dataGrid.ContextMenu?.Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(mi => (string)mi.Header == "Восстановить");
                if (miBtn != null)
                    miBtn.Visibility = Visibility.Visible;

                var context = DBEntities.GetContext();

                allStaff = context.Users
                    .Where(o => o.IsDeleted == true && o.UserData != null)
                    .Select
                    (o => new StaffViewModel
                    {
                        UserId = o.UserId,
                        UserDataId = o.UserData.UserDataId,
                        UserPhoto = o.UserData.UserPhoto,
                        FIOStaff = o.UserData.Surname + " " + o.UserData.Name + " " + o.UserData.MiddleName,
                        Email = o.UserData.Email,
                        Login = o.Login,
                        PhoneNumber = o.UserData.PhoneNumber ?? " ",
                        Role = o.Roles.NameRole ?? " ",
                        AccountStatusId = o.AccountStatusId ?? 0,
                        AccountStatus = o.AccountStatus.AccountStatusValue ?? " ",
                        IsDeleted = o.IsDeleted,
                        CreatorId = o.CreatorId ?? 2,
                        CreatedAt = o.CreatedAt ?? DateTime.MinValue
                    })
                    .ToList();

                CheckTotalPages();
                currentPage = 1;

                ComboboxesFilter.firstCombobox.SelectedIndex = 0; // Выключить фильтр

                foreach (UIElement child in ComboboxesFilter.spCboxes.Children)
                {
                    if (child is ComboboxMaterialDesignWithBorder combo && combo.Name == "cbox")
                    {
                        // Нашли нужный ComboBox
                        combo.cbox.SelectedIndex = 0;
                        break;
                    }
                }

                LoadCurrentPage();
                GeneratePaginationButtons();
            }
            catch (Exception)
            {
            }
        }

        private void spBack_MouseEnter(object sender, MouseEventArgs e)
        {
            Dickplom1.Class.Animations.OpacityAnimation(spBack, spBack.Opacity, 0.7, 0.3);
        }

        private void spBack_MouseLeave(object sender, MouseEventArgs e)
        {
            Dickplom1.Class.Animations.OpacityAnimation(spBack, spBack.Opacity, 1, 0.3);
        }

        private void spBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDeletedFilter = false; //Деактивируем флажок

            Dickplom1.Class.Musor.ShowElement(spDeletedRecords); // Включаем кнопку вернуть
            Dickplom1.Class.Musor.HideElement(spBack); // Выключаем кнопку удаленных записей

            try
            {
                MenuItem miBtn = dataGrid.dg.ContextMenu?.Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(mi => (string)mi.Header == "Восстановить");
                if (miBtn != null)
                    miBtn.Visibility = Visibility.Collapsed;

                var context = DBEntities.GetContext();

                allStaff = context.Users
                    .Where(o => o.IsDeleted == false && o.UserData != null)
                    .Select
                    (o => new StaffViewModel
                    {
                        UserId = o.UserId,
                        UserDataId = o.UserData.UserDataId,
                        UserPhoto = o.UserData.UserPhoto,
                        FIOStaff = o.UserData.Surname + " " + o.UserData.Name + " " + o.UserData.MiddleName,
                        Email = o.UserData.Email,
                        Login = o.Login,
                        PhoneNumber = o.UserData.PhoneNumber ?? " ",
                        Role = o.Roles.NameRole ?? " ",
                        AccountStatusId = o.AccountStatusId ?? 0,
                        AccountStatus = o.AccountStatus.AccountStatusValue ?? " ",
                        IsDeleted = o.IsDeleted,
                        CreatorId = o.CreatorId ?? 2,
                        CreatedAt = o.CreatedAt ?? DateTime.MinValue
                    })
                    .ToList();

                CheckTotalPages();
                currentPage = 1;

                ComboboxesFilter.firstCombobox.SelectedIndex = 0; // Выключить фильтр

                foreach (UIElement child in ComboboxesFilter.spCboxes.Children)
                {
                    if (child is ComboboxMaterialDesignWithBorder combo && combo.Name == "cbox")
                    {
                        // Нашли нужный ComboBox
                        combo.cbox.SelectedIndex = 0;
                        break;
                    }
                }

                LoadCurrentPage();
                GeneratePaginationButtons();
            }
            catch (Exception)
            {
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e) // Восстановить
        {
            var context = DBEntities.GetContext();

            try
            {
                if (dataGrid.dg.SelectedItem is StaffViewModel item)
                {
                    var selectedUser = context.Users.FirstOrDefault(f=>f.UserDataId == item.UserDataId && f.IsDeleted == true);
                    if (selectedUser != null)
                    {
                        selectedUser.IsDeleted = false;
                        context.SaveChanges();
                        RefreshItems();
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}