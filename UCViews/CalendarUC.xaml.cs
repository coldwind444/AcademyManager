using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AcademyManager.UCViews
{
    /// <summary>
    /// Interaction logic for CalendarUC.xaml
    /// </summary>
    public partial class CalendarUC : UserControl
    {
        public CalendarUC()
        {
            InitializeComponent();
        }
        public DateTime? SelectedDate
        {
            get { return (DateTime?)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(DateTime?), typeof(CalendarUC), new PropertyMetadata(null));

        // Routed event for date change
        public static readonly RoutedEvent DateChangedEvent = EventManager.RegisterRoutedEvent(
            "DateChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<DateTime?>),
            typeof(CalendarUC));

        public event RoutedPropertyChangedEventHandler<DateTime?> DateChanged
        {
            add { AddHandler(DateChangedEvent, value); }
            remove { RemoveHandler(DateChangedEvent, value); }
        }

        protected virtual void OnDateChanged(DateTime? newDate)
        {
            RoutedPropertyChangedEventArgs<DateTime?> args = new RoutedPropertyChangedEventArgs<DateTime?>(this.SelectedDate, newDate, DateChangedEvent);
            RaiseEvent(args);
        }
    }
}
