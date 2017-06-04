using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Demo3
{
    [TemplatePart(Name = EllipseTemplateName, Type = typeof(Ellipse))]
    [TemplateVisualState(GroupName = ActiveStateGroupName, Name = InactiveStateName)]
    [TemplateVisualState(GroupName = ActiveStateGroupName, Name = ActiveStateName)]
    public class ProgressRingEx : Control
    {
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(ProgressRingEx), new PropertyMetadata(default(bool), OnIsActiveChanged));

        private const string ActiveStateGroupName = "ActiveStates";

        private const string ActiveStateName = "Active";

        private const string EllipseAssistTemplateName = "PART_EllipseAssist";

        private const string EllipseTemplateName = "PART_Ellipse";

        private const string InactiveStateName = "Inactive";

        private Ellipse _ellipse;

        private EllipseAssist _ellipseAssist;

        public ProgressRingEx()
        {
            DefaultStyleKey = typeof(ProgressRingEx);
        }

        public bool IsActive
        {
            get
            {
                return (bool)GetValue(IsActiveProperty);
            }
            set
            {
                SetValue(IsActiveProperty, value);
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _ellipse = (Ellipse)GetTemplateChild(EllipseTemplateName);
            _ellipseAssist = (EllipseAssist)GetTemplateChild(EllipseAssistTemplateName);
            _ellipseAssist.AttachedEllipse = _ellipse;

            UpdateVisualState();
        }

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (ProgressRingEx)d;

            obj.UpdateVisualState();
        }

        private void UpdateVisualState()
        {
            if (IsActive)
            {
                VisualStateManager.GoToState(this, ActiveStateName, true);
            }
            else
            {
                VisualStateManager.GoToState(this, InactiveStateName, true);
            }
        }
    }
}