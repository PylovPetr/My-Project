using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lab7Lib;

namespace Lab7
{
    public partial class Form1 : Form
    {
        IShop shop;
        Timer timer;
        public Form1()
        {
            InitializeComponent();
            shop = new Shop();
            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 200;
            timer.Start();
        }

        private void RefreshLabelBuyersData()
        {
            string buyersData = "Данные о покупателях:\n";
            int buyerNumber = 0;

            foreach (string data in shop.GetBuyersStringData())
            {
                buyerNumber++;
                buyersData += string.Format("{0}. {1}\n",buyerNumber, data);
            }

            label5.Text = buyersData;
        }

        private void ModelateShop()
        {
            shop.Modelate();
            shop.NotifyBuyers();
            label4.Text = shop.GetStringData();
            RefreshLabelBuyersData();
            timer.Interval = (int)1000 / trackBar1.Value;
        }

        private void AddNewBuyer()
        {
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    WholesaleBuyer buyer = new WholesaleBuyer(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text));
                    shop.AddNewBuyer(buyer);
                    RefreshLabelBuyersData();
                }
                else
                {
                    RetailBuyer buyer = new RetailBuyer(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                    shop.AddNewBuyer(buyer);
                    RefreshLabelBuyersData();
                }
            }
        }

        private void RemoveBuyer()
        {
            int index = (int)numericUpDown1.Value - 1;
            shop.RemoveBuyer(index);
            RefreshLabelBuyersData();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ModelateShop();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox3.Visible = true;
                label6.Visible = true;
            }
            else
            {
                textBox3.Visible = false;
                label6.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewBuyer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RemoveBuyer();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
