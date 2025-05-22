using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISREC
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
            guna2ComboBoxSubcategoryPropertyType.Visible = false;
            guna2TrackBarPrice_1.Maximum = MaxPriceForTrackBars; guna2TrackBarPrice_1.SmallChange = SmallChangeForTrackBars;
            guna2TrackBarPrice_2.Maximum = MaxPriceForTrackBars; guna2TrackBarPrice_2.SmallChange = SmallChangeForTrackBars;
            //guna2PanePage1.Visible = false;
            guna2PanelPage2.Visible = false;
        }

        private bool _dragging = false;
        private Point _startPoint = new Point(0, 0);
        Func<int, int> roundValue = value => (value / 10000) * 10000; // округление
        Func<string, int, int> parseOrDefault = (input, defaultValue) =>
            int.TryParse(input.Replace(" ", ""), out int val) ? val : defaultValue;
        private int MaxPriceForTrackBars = 5000000; private int SmallChangeForTrackBars = 100000; // настройки длря трек бара

        private void guna2PanelUp_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = new Point(e.X, e.Y);
        }

        private void guna2PanelUp_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void guna2PanelUp_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - _startPoint.X, p.Y - _startPoint.Y);
            }
        }

        private void gunaButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2ButtonCollapse_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2ComboBoxPropertyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2ComboBoxSubcategoryPropertyType.Items.Clear();
            guna2ComboBoxSubcategoryPropertyType.Visible = false;
            labelTypeProperty.Text = "Тип недвижимости:";

            if (guna2ComboBoxPropertyType.SelectedItem == null) return;

            string selectedCategory = guna2ComboBoxPropertyType.SelectedItem.ToString();
            labelTypeProperty.Text = $"{selectedCategory}: ";

            switch (selectedCategory)
            {
                case "Жилая":
                    guna2ComboBoxSubcategoryPropertyType.Items.AddRange(new object[] {
                "Квартира",
                "Частный дом",
                "Апартаменты"
            });
                    guna2ComboBoxSubcategoryPropertyType.Visible = true;
                    break;

                case "Коммерческая":
                    guna2ComboBoxSubcategoryPropertyType.Items.AddRange(new object[] {
                "Офисные помещения",
                "Торговая недвижимость",
                "Склады"
            });
                    guna2ComboBoxSubcategoryPropertyType.Visible = true;
                    break;

                case "Земельные участки":
                    guna2ComboBoxSubcategoryPropertyType.Items.AddRange(new object[] {
                "Для жилой застройки",
                "Для коммерции",
                "Сельхоз земли"
            });
                    guna2ComboBoxSubcategoryPropertyType.Visible = true;
                    break;

                case "Специальные":
                    guna2ComboBoxSubcategoryPropertyType.Items.AddRange(new object[] {
                "Гаражи и парковки",
                "Промышленные объекты",
                "Социальные объекты"
            });
                    guna2ComboBoxSubcategoryPropertyType.Visible = true;
                    break;

                case "Другие":
                    guna2ComboBoxSubcategoryPropertyType.Items.AddRange(new object[] {
                "Короткая аренда",
                "Инвестиционные объекты",
                "Доли и совместная собственность"
            });
                    guna2ComboBoxSubcategoryPropertyType.Visible = true;
                    break;
            }
            guna2ComboBoxSubcategoryPropertyType.DroppedDown = true;
        }

        private void guna2ComboBoxSubcategoryPropertyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBoxSubcategoryPropertyType.SelectedItem == null) return;

            string mainCategory = guna2ComboBoxPropertyType.SelectedItem.ToString();
            string subCategory = guna2ComboBoxSubcategoryPropertyType.SelectedItem.ToString();

            labelTypeProperty.Text = $"{mainCategory}: {subCategory}";
            guna2ComboBoxSubcategoryPropertyType.Visible = false;
        }

        private void guna2ComboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2ComboBoxSubcategoryPropertyType.Items.Clear();
            guna2ComboBoxSubcategoryPropertyType.Visible = false;
            string location = "Расположение";
            labelLocation.Text = location;

            if (guna2ComboBoxLocation.SelectedItem == null) return;

            string selectedCategory = guna2ComboBoxLocation.SelectedItem.ToString();

            switch (selectedCategory)
            {
                case "Москва":
                    labelLocation.Text = $"{location}: Москва";
                    break;
                case "Московская область":
                    labelLocation.Text = $"{location}: Московская область";
                    break;
                case "Санкт-Петербург":
                    labelLocation.Text = $"{location}: Санкт-Петербург";
                    break;

            }
        }

        private void guna2ButtonReset_Click(object sender, EventArgs e)
        {
            guna2ComboBoxPropertyType.SelectedIndex = -1;
            guna2ComboBoxSubcategoryPropertyType.Items.Clear();
            guna2ComboBoxSubcategoryPropertyType.Visible = false;
            guna2ComboBoxLocation.SelectedIndex = -1;
        }

        private void guna2TrackBarPrice_1_Scroll(object sender, ScrollEventArgs e)
        {
            int roundedPrice = roundValue(guna2TrackBarPrice_1.Value); guna2TextBoxPrice_1.Text = roundedPrice.ToString();
        }

        private void guna2TrackBarPrice_2_Scroll(object sender, ScrollEventArgs e)
        {
            int roundedPrice = roundValue(guna2TrackBarPrice_2.Value); guna2TextBoxPrice_2.Text = roundedPrice.ToString();
        }

        private void guna2TextBoxPrice_1_TextChanged(object sender, EventArgs e)
        {
            guna2TrackBarPrice_1.Value = parseOrDefault(guna2TextBoxPrice_1.Text, guna2TrackBarPrice_1.Value);
        }

        private void guna2TextBoxPrice_2_TextChanged(object sender, EventArgs e)
        {
            guna2TrackBarPrice_2.Value = parseOrDefault(guna2TextBoxPrice_2.Text, guna2TrackBarPrice_2.Value);
        }

        private void guna2ButtonClients_Click(object sender, EventArgs e)
        {
            if (guna2PanelPage2.Visible == true) return; // если кнопка нажата то заново не перерисовываем

            guna2PanelPage2.Visible = true;
            guna2PanelPage1.Visible = false;
            flowLayoutPanelCards.Controls.Clear();

            List<Property> properties = GetRandomProperties(15); // кол во карточек

            foreach (var property in properties)
            {
                Guna2Panel card = CreatePropertyCard(property);
                flowLayoutPanelCards.Controls.Add(card);
            }
        }

        public class Property
        {
            public int Id { get; set; }
            public string Heading { get; set; }
            public decimal Price { get; set; }
            public string Area { get; set; }
        }

        private List<Property> GetRandomProperties(int count, string propertyType = null)
        {
            List<Property> properties = new List<Property>();
            string connectionString  = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBISREC.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = propertyType == null
                        ? $"SELECT TOP {count} * FROM [Table] ORDER BY NEWID()"
                        : $"SELECT TOP {count} * FROM [Table] WHERE Type = @Type ORDER BY NEWID()"; // рандом выборка
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (propertyType != null)
                            command.Parameters.AddWithValue("@Type", propertyType);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                properties.Add(new Property
                                {
                                    Id = reader.GetInt32(0),
                                    Heading = reader.GetString(1),
                                    Price = reader.GetDecimal(2),
                                    Area = reader.GetString(3)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}\nStackTrace: {ex.StackTrace}");
                }
            }
            return properties;
        }

        private Guna2Panel CreatePropertyCard(Property property)
        {
            Guna2Panel card = new Guna2Panel
            {
                Size = new Size(278, 128),
                BorderRadius = 4,
                FillColor = Color.White,
                BorderColor = Color.Gray,
                BorderThickness = 1,
                Margin = new Padding(5)
            };

            Guna2HtmlLabel lblHeading = new Guna2HtmlLabel
            {
                Text = property.Heading,
                Location = new Point(10, 10),
                Font = new Font("Century Gothic", 10, FontStyle.Bold),
                AutoSize = true
            };
            card.Controls.Add(lblHeading);

            Guna2HtmlLabel lblArea = new Guna2HtmlLabel
            {
                Text = $"Площадь {property.Area}",
                Location = new Point(10, 35),
                Font = new Font("Century Gothic", 9),
                AutoSize = true
            };
            card.Controls.Add(lblArea);

            Guna2HtmlLabel lblPrice = new Guna2HtmlLabel
            {
                Text = $"{property.Price:N0} Р в мес.",
                Location = new Point(10, 60),
                Font = new Font("Century Gothic", 10, FontStyle.Bold),
                ForeColor = Color.Green,
                AutoSize = true
            };
            card.Controls.Add(lblPrice);

            Guna2Button btnChat = new Guna2Button
            {
                Text = "Написать в чат",
                Size = new Size(120, 30),
                Location = new Point(10, 85),
                FillColor = Color.FromArgb(65, 88, 112),
                ForeColor = Color.White,
                BorderRadius = 4
            };
            btnChat.Click += (s, e) => MessageBox.Show($"Открыт чат для ID {property.Id}");
            card.Controls.Add(btnChat);

            return card;
        }

        private void guna2Button_Search_1_Click(object sender, EventArgs e)
        {
            guna2PanelPage1.Visible = true;
            guna2PanelPage2.Visible = false;
        }
    }
}
