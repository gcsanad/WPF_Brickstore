using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace LegoBriksz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Lego> kockak = [];
        public MainWindow()
        {
            InitializeComponent();
            tbNev.TextChanged += (s, e) =>
            {
                if (kockak.Count > 0)
                {
                    if (tbId.Text != "" && tbNev.Text != "")
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.StartsWith($"{tbId.Text}") &&
                            x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}"));

                    }
                    else
                    {
                        dgAdatok.ItemsSource = kockak.Where(x=> x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}"));
                    }

                }
                else 
                {
                    MessageBox.Show("Nincs beolvasva adatbázis!");
                }
            };

            tbId.TextChanged += (s, e) =>
            {
                if (kockak.Count > 0)
                {
                    if (tbId.Text != "")
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.StartsWith($"{tbId.Text}") && 
                            x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}"));

                    }
                    else
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.StartsWith($"{tbId.Text}"));
                    }

                }
                else
                {
                    MessageBox.Show("Nincs beolvasva adatbázis!");
                }
            };


        }

        private void btnBeolvas_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
            XDocument doc = XDocument.Load(ofd.FileName);
            foreach (var item in doc.Descendants("Item"))
            {
                    kockak.Add(new Lego($"{item.Element("ItemID").Value};{item.Element("ItemName").Value};{item.Element("CategoryName").Value};{item.Element("ColorName").Value};{item.Element("Qty").Value}"));
            }

            }
            dgAdatok.ItemsSource = kockak;
        }

       
    }
    
}