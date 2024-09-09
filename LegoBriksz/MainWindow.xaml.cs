using Microsoft.Win32;
using System.Collections.ObjectModel;
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
                    if (tbId.Text != "")
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.StartsWith($"{tbId.Text}") &&
                            x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}"));

                    }
                    else if (tbId.Text != "" && cbKat.SelectedIndex != -1)
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.StartsWith($"{tbId.Text}") &&
                            x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}") && x.CategoryName1 == $"{cbKat.SelectedItem}");

                    }
                    else if (cbKat.SelectedIndex != -1)
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}") && x.CategoryName1 == $"{cbKat.SelectedItem}");

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
                    if (tbNev.Text != "")
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.StartsWith($"{tbId.Text}") && 
                            x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}"));

                    }
                    else if (tbNev.Text != "" && cbKat.SelectedIndex != -1)
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.StartsWith($"{tbId.Text}") &&
                            x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}") && x.CategoryName1 == $"{cbKat.SelectedItem}");

                    }
                    else if (cbKat.SelectedIndex != -1)
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.ToLower().StartsWith($"{tbId.Text.ToLower()}") && x.CategoryName1 == $"{cbKat.SelectedItem}");

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
            cbKat.SelectionChanged += (s, e) =>

            {
                if (kockak.Count > 0)
                {
                    if (tbId.Text != "")
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.StartsWith($"{tbId.Text}") &&
                            x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}"));

                    }
                    else if (tbId.Text != "" && tbNev.Text != "")
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.StartsWith($"{tbId.Text}") &&
                            x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}") && x.CategoryName1 == $"{cbKat.SelectedItem}");

                    }
                    else if (tbNev.Text != "")
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}") && x.CategoryName1 == $"{cbKat.SelectedItem}");

                    }
                    else if (cbKat.SelectedIndex == -1)
                    {
                        if (tbId.Text != "" && tbNev.Text != "")
                        {
                            dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.StartsWith($"{tbId.Text}") &&
                                x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}"));

                        }
                        else if (tbNev.Text != "")
                        {
                            dgAdatok.ItemsSource = kockak.Where(x => x.ItemName1.ToLower().StartsWith($"{tbNev.Text.ToLower()}"));

                        }
                        else if (tbId.Text != "")
                        {
                            dgAdatok.ItemsSource = kockak.Where(x => x.ItemId1.StartsWith($"{tbId.Text}"));
                        }
                        else
                        {
                            dgAdatok.ItemsSource = kockak;
                        }
                    }
                    else
                    {
                        dgAdatok.ItemsSource = kockak.Where(x => x.CategoryName1.ToLower() == $"{cbKat.SelectedItem.ToString().ToLower()}");
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
            try
            {
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
            catch (Exception)
            {

                MessageBox.Show("Nem megfelelő fájl lett kiválasztva / nem lett kiválasztva fájl!");
            }
            //foreach (var item in kockak.Select(x => x.CategoryName1).Distinct())
            //{
            //    cbKat.Items.Add(item);
            //};
            cbKat.ItemsSource = kockak.OrderBy(x => x.CategoryName1).Select(x => x.CategoryName1).Distinct();
            
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cbKat.SelectedIndex = -1;
            

        }
    }
    
}