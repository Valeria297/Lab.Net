using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Lab6
{
    public class MainForm : Form
    {
        private ListBox listBoxParameters;
        private Button buttonLoad;
        private Button buttonAdd;
        private TextBox textBoxName;
        private TextBox textBoxValue;
        private Label labelName;
        private Label labelValue;
        private List<EquipmentParameter> parameters;

        public MainForm()
        {
            this.Text = "Лабораторная работа №6 - Параметры оборудования";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            parameters = new List<EquipmentParameter>();
            InitializeSampleData();
            CreateControls();
            RefreshListBox();
        }

        private void InitializeSampleData()
        {
            parameters.Add(new EquipmentParameter("Температура", "75.5 °C"));
            parameters.Add(new EquipmentParameter("Давление", "1.2 атм"));
            parameters.Add(new EquipmentParameter("Напряжение", "220 В"));
            parameters.Add(new EquipmentParameter("Ток", "5.3 А"));
            parameters.Add(new EquipmentParameter("Мощность", "1166 Вт"));
            parameters.Add(new EquipmentParameter("Частота", "50 Гц"));
            parameters.Add(new EquipmentParameter("Влажность", "45.2 %"));
            parameters.Add(new EquipmentParameter("Скорость", "1500 об/мин"));
            parameters.Add(new EquipmentParameter("Статус", "Работает"));
            parameters.Add(new EquipmentParameter("Режим", "Автоматический"));
        }

        private void CreateControls()
        {
            labelName = new Label();
            labelName.Text = "Имя параметра:";
            labelName.Location = new Point(20, 20);
            labelName.Size = new Size(100, 25);

            textBoxName = new TextBox();
            textBoxName.Location = new Point(130, 20);
            textBoxName.Size = new Size(150, 25);

            labelValue = new Label();
            labelValue.Text = "Значение:";
            labelValue.Location = new Point(20, 60);
            labelValue.Size = new Size(100, 25);

            textBoxValue = new TextBox();
            textBoxValue.Location = new Point(130, 60);
            textBoxValue.Size = new Size(150, 25);

            buttonAdd = new Button();
            buttonAdd.Text = "Добавить параметр";
            buttonAdd.Location = new Point(300, 40);
            buttonAdd.Size = new Size(150, 30);
            buttonAdd.Click += ButtonAdd_Click;

            buttonLoad = new Button();
            buttonLoad.Text = "Обновить список";
            buttonLoad.Location = new Point(300, 80);
            buttonLoad.Size = new Size(150, 30);
            buttonLoad.Click += ButtonLoad_Click;

            listBoxParameters = new ListBox();
            listBoxParameters.Location = new Point(20, 110);
            listBoxParameters.Size = new Size(430, 220);

            this.Controls.Add(labelName);
            this.Controls.Add(textBoxName);
            this.Controls.Add(labelValue);
            this.Controls.Add(textBoxValue);
            this.Controls.Add(buttonAdd);
            this.Controls.Add(buttonLoad);
            this.Controls.Add(listBoxParameters);
        }

        private void RefreshListBox()
        {
            listBoxParameters.Items.Clear();
            foreach (var param in parameters)
            {
                listBoxParameters.Items.Add(param.ToString());
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxName.Text) && 
                !string.IsNullOrWhiteSpace(textBoxValue.Text))
            {
                parameters.Add(new EquipmentParameter(textBoxName.Text, textBoxValue.Text));
                RefreshListBox();
                textBoxName.Clear();
                textBoxValue.Clear();
            }
            else
            {
                MessageBox.Show("Заполните оба поля!", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            RefreshListBox();
            MessageBox.Show("Список обновлен!", "Информация", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class EquipmentParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public EquipmentParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }
}
