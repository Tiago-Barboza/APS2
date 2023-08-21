using TodoAPP.Data;
using TodoAPP.Models;

namespace TodoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoItemPage : ContentPage
    {
        public TodoItemPage()
        {
            InitializeComponent();
        }

        async void SalvarClique(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            TodoItemDataBase database = await TodoItemDataBase.Instance;
            await database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void DeletarClique(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            TodoItemDataBase database = await TodoItemDataBase.Instance;
            await database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void CancelarClique(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}