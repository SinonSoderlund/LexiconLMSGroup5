using Microsoft.AspNetCore.Components;

namespace LMS.Blazor.Components.Layout
{
    public partial class MainLayout
    {
        [Parameter]
        public string DisplayName { get; set; } = string.Empty;



            
        public MockUser mockUser { get; set; }
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            mockUser = new MockUser(1, "Robert Karlsson");
            DisplayName = mockUser.Name;
        }  
        public class MockUser
        {
            public int Id;
            public string Name;
            public MockUser(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}
