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
            // notAllowedSeat();
            LoadSeatsDataBase();
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
        
        //void notAllowedSeat()
        //{
        //    try
        //    {
        //        //đọc danh sách từ file txt đã lưu trước đó
        //        //file lưu mặc định ở \ChuongTrinhDatGheXemPhim\bin\Debug\selected_seats.txt
        //        string filePath = "selected_seats.txt";
        //        if (System.IO.File.Exists(filePath))
        //        {
        //            // Nếu tệp đã tồn tại, thì mới đọc file
        //            string[] selectedSeatNames = System.IO.File.ReadAllLines(filePath);

        //            foreach (string seatName in selectedSeatNames)
        //            {
        //                // Tìm ghế và đặt màu đã chọn
        //                Button seatButton = tableLayoutPanelGheNormal.Controls.OfType<Button>().FirstOrDefault(button => button.Name == seatName);
        //                if (seatButton != null)
        //                {
        //                    seatButton.BackColor = Color.DarkGray;
        //                    selectedSeats.Add(seatButton);
        //                    seatButton.FlatAppearance.BorderSize = 0;
        //                    seatButton.Text = "X";
        //                }

        //                seatButton = tableLayoutPanelGheVIP.Controls.OfType<Button>().FirstOrDefault(button => button.Name == seatName);
        //                if (seatButton != null)
        //                {
        //                    seatButton.BackColor = Color.DarkGray;
        //                    selectedSeats.Add(seatButton);
        //                    seatButton.FlatAppearance.BorderSize = 0;
        //                    seatButton.Text = "X";
        //                }

        //                seatButton = tableLayoutPanelGheSweetBox.Controls.OfType<Button>().FirstOrDefault(button => button.Name == seatName);
        //                if (seatButton != null)
        //                {
        //                    seatButton.BackColor = Color.DarkGray;
        //                    selectedSeats.Add(seatButton);
        //                    seatButton.FlatAppearance.BorderSize = 0;
        //                    seatButton.Text = "X";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi: " + ex.Message);
        //    }
        //}
        bool IsSweetBox(string seatName)
        {
            return seatName.Contains("K");
        }

        private void LoadSeatsDataBase()
        {
            var context = new ModelSeat();
            try
            {
                TableLayoutPanel[] tableLayouts = { tableLayoutPanelGheNormal, tableLayoutPanelGheVIP, tableLayoutPanelGheSweetBox };
                foreach (TableLayoutPanel tableLayoutPanel in tableLayouts)
                {
                    foreach (Button seatButton in tableLayoutPanel.Controls.OfType<Button>())
                    {
                        string seatName = seatButton.Name;

                        int seatStatus = GetSeatStatusFromDatabase(context, seatName);

                        if (seatStatus == 1)
                        {
                            //Đã đặt
                            seatButton.BackColor = Color.DarkGray;
                            seatButton.FlatAppearance.BorderSize = 0;
                            seatButton.Text = "X";
                        }
                        else
                        {
                            // Ghế trống
                            if (IsSweetBox(seatButton.Name))
                            {
                                seatButton.BackColor = Color.LightPink;
                            }
                            else
                            {
                                seatButton.BackColor = Color.LightYellow;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        int GetSeatCategoryID(int tableLayoutIndex)
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
                    categoryID = 3;//sweetbox
                    break;
                default:
                    categoryID = 0;
                    break;
            }

            return categoryID;
        }
        private int GetSeatStatusFromDatabase(ModelSeat context, string seatName)
        {

            var seat = context.Seats.FirstOrDefault(s => s.TenGhe == seatName);

            if (seat != null)
            {
                return (bool)seat.TrangThai ? 1 : 0;
            }
            else
            {
                return 0;
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
                        MessageBox.Show("Ghế đã có người!");
                    }
                    else
                    {
                        MessageBox.Show("Ghế đã chọn!");

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
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void buttonThanhToan_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắn chắn thanh toán không?","Xác nhận",MessageBoxButtons.YesNo)!= DialogResult.Yes )
            {
                return;
            }

            var context = new ModelSeat();
            using (var  transaction = context.Database.BeginTransaction())
            {
                try
                {
                    int seatID = 1;
                    TableLayoutPanel[] tableLayouts = { tableLayoutPanelGheNormal, tableLayoutPanelGheVIP, tableLayoutPanelGheSweetBox };
                    foreach (TableLayoutPanel tableLayoutPanel in tableLayouts)
                    {
                        foreach (Button seatButton in tableLayoutPanel.Controls.OfType<Button>())
                        {
                            if (seatButton.BackColor == Color.DarkRed)
                            {
                                
                                string seatName = seatButton.Name;
                                int row = Convert.ToInt32(seatName[0]);
                                int seatNumber = int.Parse(seatName.Substring(1));
                                int tableLayoutIndex = tableLayouts.ToList().IndexOf(tableLayoutPanel); 
                                int seatCategoryID = GetSeatCategoryID(tableLayoutIndex); 

                                // Kiểm tra màu ghế 
                                bool seatStatus = (seatButton.BackColor == Color.DarkRed);

                                Seat newSeat = new Seat
                                {
                                    MaGhe = seatID,
                                    TenGhe = seatName,
                                    HangGhe = row,
                                    SoGhe = seatNumber,
                                    LoaiGhe = seatCategoryID,
                                    TrangThai = seatStatus
                                };

                                context.Seats.Add(newSeat);
                                seatButton.BackColor = Color.Gray;
                                seatID++;
                            }
                        }
                    }               
                    context.SaveChanges();
                    transaction.Commit();
                    MessageBox.Show("Đã thanh toán thành công!","Thành công",MessageBoxButtons.OK);
                 
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void buttonHuy_Click(object sender, EventArgs e)
        {
            var context = new ModelSeat();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    TableLayoutPanel[] tableLayouts = { tableLayoutPanelGheNormal, tableLayoutPanelGheVIP, tableLayoutPanelGheSweetBox };
                    foreach (TableLayoutPanel tableLayoutPanel in tableLayouts)
                    {
                        foreach (Button seatButton in tableLayoutPanel.Controls.OfType<Button>())
                        {
                            if (seatButton.BackColor == Color.Gray)
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
                                //lấy id từ name
                                int seatID = Convert.ToInt32(context.Seats.FirstOrDefault(p => p.TenGhe == seatButton.Name).MaGhe);

                                var removeSeatDataBase = context.Seats.FirstOrDefault(p => p.MaGhe == seatID);
                                if (removeSeatDataBase != null)
                                {
                                    context.Seats.Remove(removeSeatDataBase);
                                }
                               
                            }
                            else
                            {
                                MessageBox.Show("Không có ghế nào để hủy");
                                return;

                            }
                        }
                    }
                    context.SaveChanges();
                    transaction.Commit();
                    MessageBox.Show("Đã hủy chọn ghế");
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
