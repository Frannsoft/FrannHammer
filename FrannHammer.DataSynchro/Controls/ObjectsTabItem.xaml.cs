using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using FrannHammer.Core.Models;
using FrannHammer.DataSynchro.ViewModels;

namespace FrannHammer.DataSynchro.Controls
{
    /// <summary>
    /// Interaction logic for ObjectsTabItem.xaml
    /// </summary>
    public partial class ObjectsTabItem
    {
        public ObjectsTabItem()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            ObjectsDataGrid.DataContext = this;
        }

        public static readonly DependencyProperty DataGridItemsSourceProperty =
            DependencyProperty.Register("DataGridItemsSource", typeof(IEnumerable),
                typeof(ObjectsTabItem), new PropertyMetadata(OnItemsSourcePropertyChanged));

        public IEnumerable DataGridItemsSource
        {
            get { return GetValue(DataGridItemsSourceProperty) as IEnumerable; }
            set { SetValue(DataGridItemsSourceProperty, value); }
        }

        private static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as ObjectsTabItem;
            control?.OnItemsSourceChanged((IEnumerable)e.OldValue, (IEnumerable)e.NewValue);
        }

        private void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            // Remove handler for oldValue.CollectionChanged
            var oldValueINotifyCollectionChanged = oldValue as INotifyCollectionChanged;

            if (null != oldValueINotifyCollectionChanged)
            {
                oldValueINotifyCollectionChanged.CollectionChanged -= NewValueINotifyCollectionChanged_CollectionChanged;
            }
            // Add handler for newValue.CollectionChanged (if possible)
            var newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (null != newValueINotifyCollectionChanged)
            {
                newValueINotifyCollectionChanged.CollectionChanged += NewValueINotifyCollectionChanged_CollectionChanged;
            }
        }

        private void NewValueINotifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // No need to do anything yet.
        }

        private void ObjectsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as CharacterVm;
            if (vm == null) return;

            if (ObjectsDataGrid.SelectedIndex != -1)
            {
                var model = ObjectsDataGrid.SelectedItem as BaseModel;
                if (model == null) return;

                var editWindow = new EditWindow(model, vm.LoggedInUser);
                editWindow.ShowDialog();
            }
        }

        private void CreateObjectButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: last thing I need to figure out
            var vm = DataContext as CharacterVm;
            if (vm == null) return;

            var model = vm.CreateNewType();

            var editWindow = new EditWindow(model, vm.LoggedInUser, true);
            editWindow.ShowDialog();
        }
    }
}
