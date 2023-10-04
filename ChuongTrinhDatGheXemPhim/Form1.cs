using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChuongTrinhDatGheXemPhim.Context;

namespace ChuongTrinhDatGheXemPhim
{
    public partial class Form1 : Form
    {
        Model1 model = new Model1();
        private List<Button> selectedSeats = new List<Button>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                loadSeatNormal();
                loadSeatVIP();
                loadSeatSweetBox();

                //đọc danh sách từ file txt đã lưu trước đó
                //file lưu mặc định ở \ChuongTrinhDatGheXemPhim\bin\Debug\selected_seats.txt
                string filePath = "selected_seats.txt";
                if (System.IO.File.Exists(filePath))
                {
                    // Nếu tệp đã tồn tại, thì mới đọc file
                    string[] selectedSeatNames = System.IO.File.ReadAllLines(filePath);

                    foreach (string seatName in selectedSeatNames)
                    {
                        // Tìm ghế và đặt màu đã chọn
                        Button seatButton = tableLayoutPanelGheNormal.Controls.OfType<Button>().FirstOrDefault(button => button.Name == seatName);
                        if (seatButton != null)
                        {
                            seatButton.BackColor = Color.DarkGray;
                            selectedSeats.Add(seatButton);
                            seatButton.FlatAppearance.BorderSize = 0;
                            seatButton.Text = "X";
                        }

                        seatButton = tableLayoutPanelGheVIP.Controls.OfType<Button>().FirstOrDefault(button => button.Name == seatName);
                        if (seatButton != null)
                        {
                            seatButton.BackColor = Color.DarkGray;
                            selectedSeats.Add(seatButton);
                            seatButton.FlatAppearance.BorderSize = 0;
                            seatButton.Text = "X";
                        }

                        seatButton = tableLayoutPanelGheSweetBox.Controls.OfType<Button>().FirstOrDefault(button => button.Name == seatName);
                        if (seatButton != null)
                        {
                            seatButton.BackColor = Color.DarkGray;
                            selectedSeats.Add(seatButton);
                            seatButton.FlatAppearance.BorderSize = 0;
                            seatButton.Text = "X";
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void loadSeatNormal()
        {
            for (char row = 'A'; row <= 'D'; row++)
            {
                int totalSeatsInRow = 13;

                // Đặt ColumnCount cho tableLayoutPanelGhe dựa trên tổng số ghế trong hàng này

                for (int seatNum = 1; seatNum <= totalSeatsInRow; seatNum++)
                {
                    string seatName = row + seatNum.ToString(); // Tạo tên ghế, ví dụ: "A1", "B2",...

                    Button seatButton = new Button();
                    seatButton.Name = seatName;
                    seatButton.Text = seatName;
                    seatButton.FlatStyle = FlatStyle.Flat;
                    seatButton.BackColor = Color.LightYellow;
                    seatButton.FlatAppearance.BorderColor = Color.Green;
                    seatButton.FlatAppearance.BorderSize = 2;
                    seatButton.Click += SeatButton_Click; 


                    tableLayoutPanelGheNormal.Controls.Add(seatButton);
                }
            }
        }
        private void loadSeatVIP()
        {
            for (char row = 'E'; row <= 'J'; row++)
            {
                if (row == 'I')
                {
                    continue;
                }
                int totalSeatsInRow = 13;

                // Đặt ColumnCount cho tableLayoutPanelGhe dựa trên tổng số ghế trong hàng này

                for (int seatNum = 1; seatNum <= totalSeatsInRow; seatNum++)
                {
                    string seatName = row + seatNum.ToString(); // Tạo tên ghế, ví dụ: "A1", "B2",...

                    Button seatButton = new Button();
                    seatButton.Name = seatName;
                    seatButton.Text = seatName;
                    seatButton.FlatStyle = FlatStyle.Flat;
                    seatButton.BackColor = Color.LightYellow;
                    seatButton.FlatAppearance.BorderColor = Color.Red;
                    seatButton.FlatAppearance.BorderSize = 2;
                    seatButton.Click += SeatButton_Click; 


                    tableLayoutPanelGheVIP.Controls.Add(seatButton); 
                }
            }
        }

        private void loadSeatSweetBox()
        {
            int totalSeatsInRow = 15;

            // Đặt ColumnCount cho tableLayoutPanelGhe dựa trên tổng số ghế trong hàng này

            for (int seatNum = 1; seatNum <= totalSeatsInRow; seatNum++)
            {
                string seatName = 'K' + seatNum.ToString(); // Tạo tên ghế, ví dụ: "A1", "B2",...

                Button seatButton = new Button();
                seatButton.Name = seatName;
                seatButton.Text = seatName;
                seatButton.BackColor = Color.LightPink;
                seatButton.ForeColor = Color.White;
                seatButton.FlatStyle = FlatStyle.Flat;
                seatButton.FlatAppearance.BorderSize = 0;
                seatButton.Click += SeatButton_Click; 



                tableLayoutPanelGheSweetBox.Controls.Add(seatButton);
            }
        }

        private void SeatButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button seatButton)
                {
                    if (seatButton.BackColor == Color.LightPink) // ghế SweetBox trống
                    {
                        seatButton.BackColor = Color.DarkRed; 
                        selectedSeats.Add(seatButton); 
                    }
                    else if(seatButton.BackColor == Color.DarkRed && seatButton.FlatAppearance.BorderSize == 0) // ghế SweetBox đã chọn 
                    {
                        seatButton.BackColor = Color.LightPink;
                        selectedSeats.Remove(seatButton);
                    }
                    else if (seatButton.BackColor == Color.LightYellow) // ghế thường và VIP trống
                    {
                        seatButton.BackColor = Color.DarkRed;
                        selectedSeats.Add(seatButton);
                    }
                    else if(seatButton.BackColor == Color.DarkRed && seatButton.FlatAppearance.BorderSize == 2)// ghế thường và VIP đã chọn
                    {
                        seatButton.BackColor = Color.LightYellow;
                        selectedSeats.Remove(seatButton);
                    }
                    else
                    {
                        MessageBox.Show("Ghế đã được chọn!");
                    }
                }
            }
            catch (Exception)
            {

            } 
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //lưu danh sách những ghế đã chọn vào 1 file txt
            System.IO.File.WriteAllLines("selected_seats.txt", selectedSeats.ConvertAll(s => s.Name));
        }

        private void buttonThanhToan_Click(object sender, EventArgs e)
        {

        }
    }
}
