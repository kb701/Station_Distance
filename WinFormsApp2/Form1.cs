using System.Data;
using System.Globalization;
using System.Data.SQLite;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private Form2 _form2;

        private string _dbPath = "Data Source=TrainData.db";

        private readonly Form2ViewModel _form2ViewModel;
        public Form1(Form2ViewModel viewModel)
        {
            InitializeComponent();
            _form2ViewModel = viewModel;
            LoadData();
        }

        private void LoadData()
        {
            // CSVƒtƒ@ƒCƒ‹‚ÌƒpƒXi5‚Â‚ÌCSVƒtƒ@ƒCƒ‹j
            string[] csvFilePaths = new string[]
            {
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ã\\‹ã–{ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ã\\‹ã’†”V“‡ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ã\\‹ã‰F¡ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ã\\‹ã‹’Ãü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ã\\‹ãŒğ–ìü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ã\\‹ã|õü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ã\\‹ãÎRâ–{ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S‚¯‚¢‚Í‚ñ‚Èü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“SŠ€Œ´ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S‹g–ìü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S‹“sü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“SŒäŠü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“SR“cü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“Su–€ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“SM‹Mü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S¶‹î|õü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S¶‹îü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S¼M‹M|õü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S‘åãü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S’·–ìü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S’¹‰Hü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“V—ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“cŒ´–{ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“’‚ÌRü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“¹–¾›ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“Ş—Çü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“ì‘åãü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“ï”gü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S–¼ŒÃ‰®ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S—é­ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã‹}\\ã‹}ˆÉ’Oü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã‹}\\ã‹}‹“sü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã‹}\\ã‹}b—zü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã‹}\\ã‹}¡’Ãü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã‹}\\ã‹}_ŒËü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã‹}\\ã‹}ç—¢ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã‹}\\ã‹}•ó’Ëü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã‹}\\ã‹}–¥–Êü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã‹}\\ã‹}—’Rü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã_\\ã_‚È‚ñ‚Îü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã_\\ã_•ŒÉìü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\ã_\\ã_–{ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC‰Á‘¾ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC‹ó`ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC|õü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC‚t•lü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC‚–ìü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC¬Œ©‹´ü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC‘½“Şìü.csv",
            "C:\\GitHub\\StationDistance\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC–{ü.csv",
            };

            List<string> validcsvFilePaths = new List<string>();

            if (KeihanMainCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[0]); }
            if (NakanoshimaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[1]); }
            if (UjiCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[2]); }
            if (KeishinCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[3]); }
            if (KatanoCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[4]); }
            if (KeihanKousakuCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[5]); }
            if (IshiyamasakamotoCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[6]); }
            if (KeihannaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[7]); }
            if (KashiharaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[8]); }
            if (YoshinoCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[9]); }
            if (KintetsuKyotoCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[10]); }
            if (GoseCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[11]); }
            if (YamadaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[12]); }
            if (ShimaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[13]); }
            if (ShigiCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[14]); }
            if (IkomaKosakuCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[15]); }
            if (IkomaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[16]); }
            if (NishishigiKosakuCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[17]); }
            if (OsakaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[18]); }
            if (NaganoCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[19]); }
            if (TobaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[20]); }
            if (TenriCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[21]); }
            if (TawaramotoCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[22]); }
            if (YunoyamaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[23]); }
            if (DomyojiCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[24]); }
            if (NaraCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[25]); }
            if (MinamiOsakaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[26]); }
            if (KintetsuNambaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[27]); }
            if (NagoyaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[28]); }
            if (SuzukaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[29]); }
            if (ItamiCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[30]); }
            if (KyotoCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[31]); }
            if (KoyoCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[32]); }
            if (ImazuCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[33]); }
            if (KobeCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[34]); }
            if (SenriCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[35]); }
            if (TakarazukaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[36]); }
            if (MinoCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[37]); }
            if (ArashiyamaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[38]); }
            if (HanshinNambaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[39]); }
            if (MukogawaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[40]); }
            if (HanshinMainCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[41]); }
            if (KadaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[42]); }
            if (KukoCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[43]); }
            if (NankaiKosakuCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[44]); }
            if (TakashinohamaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[45]); }
            if (KoyaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[46]); }
            if (ShiomibashiCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[47]); }
            if (TanagawaCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[48]); }
            if (NankaiMainCheckBox.Checked == true) { validcsvFilePaths.Add(csvFilePaths[49]); }

            // Å‰‚ÌCSVƒtƒ@ƒCƒ‹‚Ìƒf[ƒ^‚ğ“Ç‚İ‚Ş
            if (validcsvFilePaths.Count == 0)
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns["Column1"].Visible = false;
            }
            else
            {
                DataTable mergedData = ReadCsv(validcsvFilePaths[0]);

                if (validcsvFilePaths.Count > 1)
                {
                    // c‚è‚ÌCSVƒtƒ@ƒCƒ‹‚ğ“Ç‚İ‚ñ‚Åƒ}[ƒW‚·‚é
                    for (int i = 1; i < validcsvFilePaths.Count; i++)
                    {
                        var csvData = ReadCsv(validcsvFilePaths[i]);
                        mergedData = MergeDataTables(mergedData, csvData);
                    }
                }

                // ‚±‚±‚ÅƒtƒBƒ‹ƒ^ˆ—
                if (double.TryParse(textBoxFom1Min.Text, out double minValue) &&
                    double.TryParse(textBoxFom1Max.Text, out double maxValue))
                {
                    var filteredRows = mergedData.AsEnumerable()
                        .Where(row => double.TryParse(row["‰wŠÔ‹——£ (km)"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value) &&
                                      value >= minValue && value <= maxValue);

                    if (filteredRows.Any())
                    {
                        mergedData = filteredRows.CopyToDataTable();
                    }
                    else
                    {
                        mergedData.Rows.Clear();
                        MessageBox.Show("ŒŸõŒ‹‰Ê‚Í0Œ‚Å‚·B");
                    }
                }

                // DataGridView‚Éƒf[ƒ^‚ğİ’è
                dataGridView1.DataSource = mergedData;

                // “S“¹‰ïĞ•Ê‚ÉŒ”‚ğWŒv
                var groupedCounts = mergedData.AsEnumerable()
                    .GroupBy(row => row.Field<string>("“S“¹‰ïĞ"))
                    .ToDictionary(g => g.Key, g => g.Count());

                // Šeƒ‰ƒxƒ‹‚ÉŒ”‚ğ”½‰fiŠY“–‚ª‚ ‚ê‚Îj
                if (groupedCounts.ContainsKey("‹ã"))
                {
                    string percentage = 86 > 0 ? $" ({(groupedCounts["‹ã"] * 100.0 / 86):F1}%)" : "";
                    labelKeihanCount.Text = $"‹ã: {groupedCounts["‹ã"]} Œ / 86Œ{percentage}";
                }
                else
                {
                    labelKeihanCount.Text = $"‹ã: 0 Œ / 86Œ (0%)";
                }

                if (groupedCounts.ContainsKey("‹ß“S"))
                {
                    string percentage = 285 > 0 ? $" ({(groupedCounts["‹ß“S"] * 100.0 / 285):F1}%)" : "";
                    labelKintetsuCount.Text = $"‹ß“S: {groupedCounts["‹ß“S"]} Œ / 285Œ{percentage}";
                }
                else
                {
                    labelKintetsuCount.Text = $"‹ß“S: 0 Œ / 285Œ (0%)";
                }

                if (groupedCounts.ContainsKey("ã‹}"))
                {
                    string percentage = 90 > 0 ? $" ({(groupedCounts["ã‹}"] * 100.0 / 90):F1}%)" : "";
                    labelHankyuCount.Text = $"ã‹}: {groupedCounts["ã‹}"]} Œ / 90Œ{percentage}";
                }
                else
                {
                    labelHankyuCount.Text = $"ã‹}: 0 Œ / 90Œ (0%)";
                }

                if (groupedCounts.ContainsKey("ã_"))
                {
                    string percentage = 45 > 0 ? $" ({(groupedCounts["ã_"] * 100.0 / 45):F1}%)" : "";
                    labelHanshinCount.Text = $"ã_: {groupedCounts["ã_"]} Œ/ 45Œ{percentage} ";
                }
                else
                {
                    labelHanshinCount.Text = $"ã_: 0 Œ / 45Œ (0%)";
                }

                if (groupedCounts.ContainsKey("“ìŠC"))
                {
                    string percentage = 100 > 0 ? $" ({(groupedCounts["“ìŠC"] * 100.0 / 100):F1}%)" : "";
                    labelNankaiCount.Text = $"“ìŠC: {groupedCounts["“ìŠC"]} Œ / 100Œ{percentage}";
                }
                else
                {
                    labelNankaiCount.Text = $"“ìŠC: 0 Œ / 100Œ (0%)";
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["Column1"].Value = i + 1;
                }

                dataGridView1.Columns["Column1"].Width = 50;
            }

            if (checkBox1.Checked == true)
            {
                Coloring();
            }
            else
            {
                UnColoring();
            }
        }



        private DataTable ReadCsv(string filePath)
        {
            DataTable dataTable = new DataTable();

            try
            {

                using (var reader = new StreamReader(filePath))
                {
                    bool headerRead = false;
                    string previousStation = string.Empty;
                    string previousLine = string.Empty;
                    string previousCompany = string.Empty;

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        // ƒwƒbƒ_[‚ğ“Ç‚İ‚ŞiÅ‰‚Ìsj
                        if (!headerRead)
                        {
                            dataGridView1.Columns["Column1"].Visible = true;
                            dataTable.Columns.Add("“S“¹‰ïĞ");
                            dataTable.Columns.Add("˜Hü–¼");
                            dataTable.Columns.Add("‹æŠÔ");
                            dataTable.Columns.Add("‰wŠÔ‹——£ (km)");
                            headerRead = true;
                        }

                        // Å‰‚Ì‰w‚Ìƒf[ƒ^‚ğ•Û‘¶
                        string company = values[0].Trim();
                        string lineName = values[1].Trim();
                        string stationName = values[2].Trim();

                        // ”’l‚Ì‰ğÍ‚ğ•¶‰»‚ÉˆË‘¶‚µ‚È‚¢‚æ‚¤‚É•ÏX
                        double distance = double.Parse(values[3].Trim(), CultureInfo.InvariantCulture);  // InvariantCulture‚ğg—p

                        // Å‰‚Ì‰w‚ÆŸ‚Ì‰w‚ÌƒyƒA‚ğì¬
                        if (!string.IsNullOrEmpty(previousStation))
                        {
                            string stationPair = $"{previousStation}[{stationName}";
                            dataTable.Rows.Add(previousCompany, previousLine, stationPair, distance);
                        }

                        // Œ»İ‚Ì‰w‚Ìî•ñ‚ğŸ‰ñ‚Ìƒ‹[ƒv‚Åg—p‚·‚é‚½‚ß‚É•Û‘¶
                        previousStation = stationName;
                        previousCompany = company;
                        previousLine = lineName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CSV“Ç‚İ‚İƒGƒ‰[: {ex.Message}");
            }

            return dataTable;
        }

        // 2‚Â‚ÌDataTable‚ğƒ}[ƒW‚·‚éƒƒ\ƒbƒh
        private DataTable MergeDataTables(DataTable table1, DataTable table2)
        {
            DataTable mergedTable = table1.Copy();

            // 2‚Â–Ú‚Ìƒe[ƒuƒ‹‚Ìs‚ğ’Ç‰Á
            foreach (DataRow row in table2.Rows)
            {
                mergedTable.ImportRow(row);
            }

            return mergedTable;
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void KeihanAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (KeihanAllCheckBox.Checked == true)
            {
                KeihanMainCheckBox.Checked = true;
                NakanoshimaCheckBox.Checked = true;
                UjiCheckBox.Checked = true;
                IshiyamasakamotoCheckBox.Checked = true;
                KeihanKousakuCheckBox.Checked = true;
                KeishinCheckBox.Checked = true;
                KatanoCheckBox.Checked = true;
            }
            else
            {
                KeihanMainCheckBox.Checked = false;
                NakanoshimaCheckBox.Checked = false;
                UjiCheckBox.Checked = false;
                IshiyamasakamotoCheckBox.Checked = false;
                KeihanKousakuCheckBox.Checked = false;
                KeishinCheckBox.Checked = false;
                KatanoCheckBox.Checked = false;
            }
        }

        private void HankyuAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (HankyuAllCheckBox.Checked == true)
            {
                KyotoCheckBox.Checked = true;
                KobeCheckBox.Checked = true;
                TakarazukaCheckBox.Checked = true;
                SenriCheckBox.Checked = true;
                ArashiyamaCheckBox.Checked = true;
                ItamiCheckBox.Checked = true;
                ImazuCheckBox.Checked = true;
                KoyoCheckBox.Checked = true;
                MinoCheckBox.Checked = true;
            }
            else
            {
                KyotoCheckBox.Checked = false;
                KobeCheckBox.Checked = false;
                TakarazukaCheckBox.Checked = false;
                SenriCheckBox.Checked = false;
                ArashiyamaCheckBox.Checked = false;
                ItamiCheckBox.Checked = false;
                ImazuCheckBox.Checked = false;
                KoyoCheckBox.Checked = false;
                MinoCheckBox.Checked = false;
            }
        }

        private void HanshinAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (HanshinAllCheckBox.Checked == true)
            {
                HanshinMainCheckBox.Checked = true;
                HanshinNambaCheckBox.Checked = true;
                MukogawaCheckBox.Checked = true;
            }
            else
            {
                HanshinMainCheckBox.Checked = false;
                HanshinNambaCheckBox.Checked = false;
                MukogawaCheckBox.Checked = false;
            }
        }

        private void NankaiAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NankaiAllCheckBox.Checked == true)
            {
                NankaiMainCheckBox.Checked = true;
                KoyaCheckBox.Checked = true;
                ShiomibashiCheckBox.Checked = true;
                KukoCheckBox.Checked = true;
                KadaCheckBox.Checked = true;
                TakashinohamaCheckBox.Checked = true;
                TanagawaCheckBox.Checked = true;
                NankaiKosakuCheckBox.Checked = true;
            }
            else
            {
                NankaiMainCheckBox.Checked = false;
                KoyaCheckBox.Checked = false;
                ShiomibashiCheckBox.Checked = false;
                KukoCheckBox.Checked = false;
                KadaCheckBox.Checked = false;
                TakashinohamaCheckBox.Checked = false;
                TanagawaCheckBox.Checked = false;
                NankaiKosakuCheckBox.Checked = false;
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["Column1"].Value = i + 1;
            }

            dataGridView1.Columns["Column1"].Width = 50;

            if (checkBox1.Checked == true)
            {
                Coloring();
            }
            else
            {
                UnColoring();
            }
        }

        private void KintetsuAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (KintetsuAllCheckBox.Checked == true)
            {
                OsakaCheckBox.Checked = true;
                KintetsuNambaCheckBox.Checked = true;
                KintetsuKyotoCheckBox.Checked = true;
                NaraCheckBox.Checked = true;
                MinamiOsakaCheckBox.Checked = true;
                KashiharaCheckBox.Checked = true;
                KeihannaCheckBox.Checked = true;
                IkomaCheckBox.Checked = true;
                IkomaKosakuCheckBox.Checked = true;
                NishishigiKosakuCheckBox.Checked = true;
                YoshinoCheckBox.Checked = true;
                NaganoCheckBox.Checked = true;
                ShigiCheckBox.Checked = true;
                DomyojiCheckBox.Checked = true;
                TawaramotoCheckBox.Checked = true;
                GoseCheckBox.Checked = true;
                TenriCheckBox.Checked = true;
                NagoyaCheckBox.Checked = true;
                YunoyamaCheckBox.Checked = true;
                SuzukaCheckBox.Checked = true;
                YamadaCheckBox.Checked = true;
                TobaCheckBox.Checked = true;
                ShimaCheckBox.Checked = true;
            }
            else
            {
                OsakaCheckBox.Checked = false;
                KintetsuNambaCheckBox.Checked = false;
                KintetsuKyotoCheckBox.Checked = false;
                NaraCheckBox.Checked = false;
                MinamiOsakaCheckBox.Checked = false;
                KashiharaCheckBox.Checked = false;
                KeihannaCheckBox.Checked = false;
                IkomaCheckBox.Checked = false;
                IkomaKosakuCheckBox.Checked = false;
                NishishigiKosakuCheckBox.Checked = false;
                YoshinoCheckBox.Checked = false;
                NaganoCheckBox.Checked = false;
                ShigiCheckBox.Checked = false;
                DomyojiCheckBox.Checked = false;
                TawaramotoCheckBox.Checked = false;
                GoseCheckBox.Checked = false;
                TenriCheckBox.Checked = false;
                NagoyaCheckBox.Checked = false;
                YunoyamaCheckBox.Checked = false;
                SuzukaCheckBox.Checked = false;
                YamadaCheckBox.Checked = false;
                TobaCheckBox.Checked = false;
                ShimaCheckBox.Checked = false;
            }
        }

        private void ExportToCsv(string filePath)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("ƒGƒNƒXƒ|[ƒg‚·‚éƒf[ƒ^‚ª‚ ‚è‚Ü‚¹‚ñB");
                return;
            }

            try
            {
                using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    // ƒwƒbƒ_[s
                    var headers = dataGridView1.Columns
                        .Cast<DataGridViewColumn>()
                        .Where(c => c.Visible)
                        .Select(c => c.HeaderText)
                        .ToArray();
                    writer.WriteLine(string.Join(",", headers));

                    // Šeƒf[ƒ^s
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            var cells = row.Cells
                                .Cast<DataGridViewCell>()
                                .Where(c => c.Visible)
                                .Select(c => c.Value?.ToString().Replace(",", ""))
                                .ToArray();
                            writer.WriteLine(string.Join(",", cells));
                        }
                    }
                }

                MessageBox.Show("CSVƒtƒ@ƒCƒ‹‚ÌƒGƒNƒXƒ|[ƒg‚ªŠ®—¹‚µ‚Ü‚µ‚½B");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CSV‚ÌƒGƒNƒXƒ|[ƒg’†‚ÉƒGƒ‰[‚ª”­¶‚µ‚Ü‚µ‚½: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_form2 == null || _form2.IsDisposed)
            {
                _form2 = new Form2(_form2ViewModel);
            }
            // ƒ‚[ƒ_ƒ‹•\¦
            if (_form2.ShowDialog() == DialogResult.OK)
            {
                if (_form2ViewModel.IsManualInputEnabled == true)
                {
                    textBoxFom1Min.Text = _form2.MinValue;
                    textBoxFom1Max.Text = _form2.MaxValue;
                }
                if (_form2ViewModel.IsAutoInputEnabled == true)
                {
                    switch (_form2ViewModel.SelectedOption)
                    {
                        case "0.0`0.9":
                            textBoxFom1Min.Text = "0.0";
                            textBoxFom1Max.Text = "0.9";
                            break;
                        case "1.0`1.9":
                            textBoxFom1Min.Text = "1.0";
                            textBoxFom1Max.Text = "1.9";
                            break;
                        case "2.0`2.9":
                            textBoxFom1Min.Text = "2.0";
                            textBoxFom1Max.Text = "2.9";
                            break;
                        case "3.0`3.9":
                            textBoxFom1Min.Text = "3.0";
                            textBoxFom1Max.Text = "3.9";
                            break;
                        case "4.0`4.9":
                            textBoxFom1Min.Text = "4.0";
                            textBoxFom1Max.Text = "4.9";
                            break;
                        case "5.0`5.9":
                            textBoxFom1Min.Text = "5.0";
                            textBoxFom1Max.Text = "5.9";
                            break;
                        case "6.0`6.9":
                            textBoxFom1Min.Text = "6.0";
                            textBoxFom1Max.Text = "6.9";
                            break;
                        case "7.0`7.9":
                            textBoxFom1Min.Text = "7.0";
                            textBoxFom1Max.Text = "7.9";
                            break;
                    }
                }

            }
        }

        private void Coloring()
        {
            // ‰wŠÔ‹——£‚É‰‚¶‚Äs‚ÉF‚ğ‚Â‚¯‚éˆ—
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (double.TryParse(row.Cells["‰wŠÔ‹——£ (km)"].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double distance))
                    {
                        if (distance >= 0.0 && distance <= 0.9)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                        }
                        else if (distance >= 1.0 && distance <= 1.9)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else if (distance >= 2.0 && distance <= 2.9)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                        }
                        else if (distance >= 3.0 && distance <= 3.9)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightPink;
                        }
                        else if (distance >= 4.0 && distance <= 4.9)
                        {
                            row.DefaultCellStyle.BackColor = Color.Orange;
                        }
                        else if (distance >= 5.0 && distance <= 5.9)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightSalmon;
                        }
                        // •K—v‚É‰‚¶‚Ä’Ç‰Á
                    }
                }
            }
        }

        private void UnColoring()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = Color.White;
                }
            }
        }

        private void exportBtn_Click(object sender, EventArgs e)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "CSVƒtƒ@ƒCƒ‹ (*.csv)|*.csv";
                    saveFileDialog.Title = "CSVƒtƒ@ƒCƒ‹‚Ì•Û‘¶";
                    saveFileDialog.FileName = "o—ÍŒ‹‰Ê.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToCsv(saveFileDialog.FileName);
                    }
                }
            }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Coloring();
            }
            else
            {
                UnColoring();
            }
            LoadData();
        }

        private void EnsureDatabase()
        {
            using (var conn = new SQLiteConnection(_dbPath))
            {
                conn.Open();
                string sql = @"
            CREATE TABLE IF NOT EXISTS TrainSections (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Company TEXT,
                Line TEXT,
                Section TEXT,
                Distance REAL
            );";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveDataToDatabase()
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("•Û‘¶‚·‚éƒf[ƒ^‚ª‚ ‚è‚Ü‚¹‚ñB");
                return;
            }

            // DB‚Æƒe[ƒuƒ‹‚ğ€”õ
            EnsureDatabase();

            using (var conn = new SQLiteConnection(_dbPath))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        // šš³í‚É“o˜^šš
                        cmd.CommandText = "INSERT INTO TrainSections (Company, Line, Section, Distance) VALUES (@company, @line, @section, @distance)";

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsNewRow) continue;

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@company", row.Cells["“S“¹‰ïĞ"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@line", row.Cells["˜Hü–¼"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@section", row.Cells["‹æŠÔ"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@distance", Convert.ToDouble(row.Cells["‰wŠÔ‹——£ (km)"].Value));

                            cmd.ExecuteNonQuery();
                        }
                        // šš³í‚É“o˜^@I‚í‚èšš

                        // ššƒ[ƒ‹ƒoƒbƒNŠm”F—pƒR[ƒhšš
                        // ‡@ •s³‚Èƒf[ƒ^iDistance—ñ‚É•¶š—ñ‚ğ“ü‚ê‚ÄƒGƒ‰[‚ğ‹N‚±‚·j
                        cmd.CommandText = "INSERT INTO TrainSections (Id, Company, Line, Section, Distance) VALUES (235, 'JR', 'Yamanote', 'Shinjuku - Ikebukuro', 5.0)";
                        cmd.ExecuteNonQuery();  // ‚±‚±‚Å—áŠO‚ª”­¶
                        // ššƒ[ƒ‹ƒoƒbƒNŠm”F—pƒR[ƒh@I‚í‚èšš
                    }

                    transaction.Commit();
                }
            }

            MessageBox.Show("ƒf[ƒ^ƒx[ƒX‚Ö‚Ì“o˜^‚ªŠ®—¹‚µ‚Ü‚µ‚½B");
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            SaveDataToDatabase();
        }
    }

    }