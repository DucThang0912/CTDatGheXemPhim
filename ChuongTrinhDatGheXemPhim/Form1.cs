using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        ModelSeat modelSeat = new ModelSeat();
        List<Button> selectedSeats = new List<Button>();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadSeatNormal();
            loadSeatVIP();
            loadSeatSweetBox();
            notAllowedSeat();
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
        
        void notAllowedSeat()
        {
            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        bool IsSweetBox(string seatName)
        {
            return seatName.Contains("K");
        }
        void resetSelectedSeats()
        {
            try
            {
                foreach (var seatButton in selectedSeats)
                {
                    if (seatButton.BackColor == Color.DarkGray && seatButton.Text == "X")
                    {
                        if (IsSweetBox(seatButton.Name))
                        {
                            seatButton.BackColor = Color.LightPink; 
                        }
                        else
                        {
                            seatButton.BackColor = Color.LightYellow; 
                        }
                        seatButton.Text = seatButton.Name;
                        seatButton.FlatAppearance.BorderSize = 2; 
                    }
                }
                selectedSeats.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
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
                    else if(seatButton.BackColor == Color.Gray && seatButton.Text == "X")
                    {
                        MessageBox.Show("Ghế đã được chọn!");
                    }
                    else
                    {
                        MessageBox.Show("Ghế đã có người!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            } 
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           if(MessageBox.Show("Bạn có muốn đóng ứng dụng?","Xác nhận",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //lưu danh sách những ghế đã chọn vào 1 file txt
                System.IO.File.WriteAllLines("selected_seats.txt", selectedSeats.ConvertAll(s => s.Name));
            }
            else
            {
                e.Cancel = true;
            }
        }
        int GetSeatCategoryIDFromPosition(int tableLayoutIndex)
        {
            int categoryID;

            switch (tableLayoutIndex)
            {
                case 0:
                    categoryID = 1; //thường
                    break;
                case 1: 
                    categoryID = 2; //vip 
                    break;
                case 2: 
                    categoryID = 3;//sweebox
                    break;
                default:
                    categoryID = 0;
                    break;
            }

            return categoryID;
        }
        private void buttonThanhToan_Click(object sender, EventArgs e)
        {
            var context = new ModelSeat();
            using (var  transaction = context.Database.BeginTransaction())
            {
                try
                {
                    TableLayoutPanel[] tableLayouts = { tableLayoutPanelGheNormal, tableLayoutPanelGheVIP, tableLayoutPanelGheSweetBox };
                    foreach (TableLayoutPanel tableLayoutPanel in tableLayouts)
                    {
                        foreach (Button seatButton in tableLayoutPanel.Controls.OfType<Button>())
                        {
                            if (seatButton.BackColor == Color.DarkRed)
                            {
                                // Lấy thông tin từ tên ghế
                                int seatID = context.Seats.Max(s => (int?)s.MaGhe) ?? 0 + 1;
                                string seatName = seatButton.Name;
                                int row = Convert.ToInt32(seatName[0]);
                                int seatNumber = int.Parse(seatName.Substring(1));
                                // Lấy ID của loại ghế (ví dụ: thường, vip, sweetbox)
                                int tableLayoutIndex = tableLayouts.ToList().IndexOf(tableLayoutPanel); // Xác định vị trí của TableLayoutPanel
                                int seatCategoryID = GetSeatCategoryIDFromPosition(tableLayoutIndex); // Thay thế bằng cách lấy ID tương ứng từ người dùng

                                // Kiểm tra màu ghế để xác định trạng thái
                                bool seatStatus = (seatButton.BackColor == Color.DarkRed);

                                // Tạo đối tượng Seat và thêm vào DbSet
                                Seat newSeat = new Seat
                                {
                                    MaGhe = seatID,
                                    TenGhe = seatName,
                                    HangGhe = row,
                                    SoGhe = seatNumber,
                                    LoaiGhe = seatCategoryID,
                                    TrangThai = seatStatus
                                };

                                // Lưu đối tượng Seat vào CSDL (bạn cần cấu hình DbContext và DbSet cho bảng Seats)
                                context.Seats.Add(newSeat);
                                seatButton.BackColor = Color.Gray;
                                //string query = $"DBCC CHECKIDENT ('[MaGhe]', RESEED, {seatID - 1});";
                                //context.Database.ExecuteSqlCommand(query);
                            }
                        }
                    }
                    
                    // Lưu các ghế vào CSDL
                    context.SaveChanges();
                    transaction.Commit();
                    resetSelectedSeats();
                    MessageBox.Show("Đã lưu vào CSDL");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }     
    }
}
