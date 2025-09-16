using Assignment_11._1.Context;
using Assignment_11._1.Models;
using System.Windows;

namespace Assignment_11._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookContext context;

        public MainWindow(BookContext _context)
        {
            InitializeComponent();
            context = _context;
            BooksDG.ItemsSource = context.Books.ToList();
        }

        Book selectedBook = new();

        private void AddBook(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
            ISBNBox.Text = Guid.NewGuid().ToString();
            SubmitButton.IsEnabled = true;
            ISBNBox.IsReadOnly = true;
        }
        private void EditBook(object sender, RoutedEventArgs e)
        {
            selectedBook = (sender as FrameworkElement).DataContext as Book;

            if (selectedBook == null)
            {
                MessageBox.Show("Select a product to edit");
                return;
            }

            ISBNBox.Text = selectedBook.ISBN.ToString();
            TitleBox.Text = selectedBook.Title;
            AuthorBox.Text = selectedBook.Author;
            DescriptionBox.Text = selectedBook.Description;

            SubmitButton.IsEnabled = true;
            ISBNBox.IsReadOnly = true;
        }
        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            Book book = (sender as FrameworkElement).DataContext as Book;

            if (book == null)
            {
                MessageBox.Show("Select a product to delete");
                return;
            }

            context.Books.Remove(book);
            context.SaveChanges();
            BooksDG.ItemsSource = context.Books.ToList();

        }
        private void SubmitChange(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TitleBox.Text) && string.IsNullOrEmpty(ISBNBox.Text) && string.IsNullOrEmpty(AuthorBox.Text) && string.IsNullOrEmpty(DescriptionBox.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            Book newBook = new();

            newBook.Title = TitleBox.Text;
            newBook.Author = AuthorBox.Text;
            newBook.Description = DescriptionBox.Text;
            newBook.ISBN = ISBNBox.Text;

            if(selectedBook.ISBN == newBook.ISBN)
            {
                context.Remove(selectedBook);
                context.Add(newBook);
            }
            else
            {
                context.Books.Add(newBook);
            }

            context.SaveChanges();
            BooksDG.ItemsSource = context.Books.ToList();
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            TitleBox.Clear();
            ISBNBox.Clear();
            AuthorBox.Clear();
            DescriptionBox.Clear();
            SubmitButton.IsEnabled = false;
            ISBNBox.IsReadOnly = false;
        }
    }
}