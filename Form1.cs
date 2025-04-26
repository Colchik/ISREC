using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        }

        private bool _dragging = false;
        private Point _startPoint = new Point(0, 0);
        private void panelUp_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = new Point(e.X, e.Y);
        }

        private void panelUp_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void panelUp_MouseMove(object sender, MouseEventArgs e)
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
    }
}
