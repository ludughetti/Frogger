using Utils;

namespace Editor.Tests.EditMode.Mocked
{
    public class FakeApplicationHandler : IApplicationHandler
    {
        public bool QuitCalled { get; private set; }

        public void Quit()
        {
            QuitCalled = true;
        }
    }
}