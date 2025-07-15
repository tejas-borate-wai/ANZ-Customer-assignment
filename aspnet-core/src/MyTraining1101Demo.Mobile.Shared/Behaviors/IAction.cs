using Xamarin.Forms.Internals;

namespace MyTraining1101Demo.Behaviors
{
    [Preserve(AllMembers = true)]
    public interface IAction
    {
        bool Execute(object sender, object parameter);
    }
}