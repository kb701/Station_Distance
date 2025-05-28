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

        private Dictionary<CheckBox, string> checkBoxToPath;

        // “S“¹‰ïĞ‚²‚Æ‚Ì‘SŒ”i’è”j
        private readonly Dictionary<string, int> totalCounts = new()
        {
            ["‹ã"] = 86,
            ["‹ß“S"] = 285,
            ["ã‹}"] = 90,
            ["ã_"] = 45,
            ["“ìŠC"] = 100,
        };
        public Form1(Form2ViewModel viewModel)
        {
            InitializeComponent();
            _form2ViewModel = viewModel;


            // ‰wŠÔ‹——£CSVƒtƒ@ƒCƒ‹ƒpƒX‚Æƒ`ƒFƒbƒNƒ{ƒbƒNƒX‚Ì‘Î‰
            checkBoxToPath = new Dictionary<CheckBox, string>
            {
             { KeihanMainCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ã\\‹ã–{ü.csv" },
             { NakanoshimaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ã\\‹ã’†”V“‡ü.csv" },
             { UjiCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ã\\‹ã‰F¡ü.csv" },
             { KeishinCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ã\\‹ã‹’Ãü.csv" },
             { KatanoCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ã\\‹ãŒğ–ìü.csv" },
             { KeihanKousakuCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ã\\‹ã|õü.csv" },
             { IshiyamasakamotoCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ã\\‹ãÎRâ–{ü.csv" },

             { KeihannaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S‚¯‚¢‚Í‚ñ‚Èü.csv" },
             { KashiharaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“SŠ€Œ´ü.csv" },
             { YoshinoCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S‹g–ìü.csv" },
             { KintetsuKyotoCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S‹“sü.csv" },
             { GoseCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“SŒäŠü.csv" },
             { YamadaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“SR“cü.csv" },
             { ShimaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“Su–€ü.csv" },
             { ShigiCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“SM‹Mü.csv" },
             { IkomaKosakuCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S¶‹î|õü.csv" },
             { IkomaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S¶‹îü.csv" },
             { NishishigiKosakuCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S¼M‹M|õü.csv" },
             { OsakaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S‘åãü.csv" },
             { NaganoCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S’·–ìü.csv" },
             { TobaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S’¹‰Hü.csv" },
             { TenriCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“V—ü.csv" },
             { TawaramotoCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“cŒ´–{ü.csv" },
             { YunoyamaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“’‚ÌRü.csv" },
             { DomyojiCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“¹–¾›ü.csv" },
             { NaraCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“Ş—Çü.csv" },
             { MinamiOsakaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“ì‘åãü.csv" },
             { KintetsuNambaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S“ï”gü.csv" },
             { NagoyaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S–¼ŒÃ‰®ü.csv" },
             { SuzukaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\‹ß“S\\‹ß“S—é­ü.csv" },

             { ItamiCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã‹}\\ã‹}ˆÉ’Oü.csv" },
             { KyotoCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã‹}\\ã‹}‹“sü.csv" },
             { KoyoCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã‹}\\ã‹}b—zü.csv" },
             { ImazuCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã‹}\\ã‹}¡’Ãü.csv" },
             { KobeCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã‹}\\ã‹}_ŒËü.csv" },
             { SenriCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã‹}\\ã‹}ç—¢ü.csv" },
             { TakarazukaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã‹}\\ã‹}•ó’Ëü.csv" },
             { MinoCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã‹}\\ã‹}–¥–Êü.csv" },
             { ArashiyamaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã‹}\\ã‹}—’Rü.csv" },

             { HanshinNambaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã_\\ã_‚È‚ñ‚Îü.csv" },
             { MukogawaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã_\\ã_•ŒÉìü.csv" },
             { HanshinMainCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\ã_\\ã_–{ü.csv" },

             { KadaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC‰Á‘¾ü.csv" },
             { KukoCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC‹ó`ü.csv" },
             { NankaiKosakuCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC|õü.csv" },
             { TakashinohamaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC‚t•lü.csv" },
             { KoyaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC‚–ìü.csv" },
             { ShiomibashiCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC¬Œ©‹´ü.csv" },
             { TanagawaCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC‘½“Şìü.csv" },
             { NankaiMainCheckBox, "..\\..\\..\\..\\‰wŠÔ‹——£\\“ìŠC\\“ìŠC–{ü.csv" },
            };

            LoadData();
        }

        private void LoadData()
        {
            var selectedCsvPaths = GetSelectedCsvPaths();

            if (selectedCsvPaths.Count == 0)
            {
                ClearDataGrid();
                return;
            }

            var mergedData = LoadAndMergeCsvFiles(selectedCsvPaths);
            mergedData = ApplyDistanceFilter(mergedData);

            if (mergedData.Rows.Count == 0)
            {
                MessageBox.Show("ŒŸõŒ‹‰Ê‚Í0Œ‚Å‚·B");
            }

            UpdateDataGrid(mergedData);
            UpdateRailwayCountLabels(mergedData);

            if (checkBox1.Checked)
            {
                Coloring();
            }
            else
            {
                UnColoring();
            }
        }

        // CSV‘I‘ğƒƒWƒbƒN
        private List<string> GetSelectedCsvPaths()
        {
            return checkBoxToPath
                .Where(kvp => kvp.Key.Checked)
                .Select(kvp => kvp.Value)
                .ToList();
        }

        // ƒf[ƒ^“Ç‚İ‚İ‚Æƒ}[ƒW
        private DataTable LoadAndMergeCsvFiles(List<string> paths)
        {
            var merged = ReadCsv(paths[0]);

            for (int i = 1; i < paths.Count; i++)
            {
                var data = ReadCsv(paths[i]);
                merged = MergeDataTables(merged, data);
            }

            return merged;
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

        // ƒtƒBƒ‹ƒ^ˆ—
        private DataTable ApplyDistanceFilter(DataTable data)
        {
            if (!double.TryParse(textBoxFom1Min.Text, out double min) ||
                !double.TryParse(textBoxFom1Max.Text, out double max))
            {
                return data;
            }

            var filtered = data.AsEnumerable()
                .Where(row => double.TryParse(row["‰wŠÔ‹——£ (km)"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value)
                              && value >= min && value <= max);

            return filtered.Any() ? filtered.CopyToDataTable() : data.Clone();
        }

        // ƒf[ƒ^•\¦XV
        private void UpdateDataGrid(DataTable data)
        {
            dataGridView1.DataSource = data;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["Column1"].Value = i + 1;
            }

            dataGridView1.Columns["Column1"].Width = 50;
        }

        // Œ”ƒ‰ƒxƒ‹•\¦XV
        private void UpdateRailwayCountLabels(DataTable data)
        {
            var grouped = data.AsEnumerable()
                .GroupBy(row => row.Field<string>("“S“¹‰ïĞ"))
                .ToDictionary(g => g.Key, g => g.Count());

            labelKeihanCount.Text = FormatLabel("‹ã", grouped);
            labelKintetsuCount.Text = FormatLabel("‹ß“S", grouped);
            labelHankyuCount.Text = FormatLabel("ã‹}", grouped);
            labelHanshinCount.Text = FormatLabel("ã_", grouped);
            labelNankaiCount.Text = FormatLabel("“ìŠC", grouped);
        }

        private string FormatLabel(string company, Dictionary<string, int> grouped)
        {
            int count = grouped.ContainsKey(company) ? grouped[company] : 0;
            int total = totalCounts[company];
            string percent = total > 0 ? $" ({(count * 100.0 / total):F1}%)" : "";
            return $"{company}: {count} Œ / {total}Œ{percent}";
        }

        // ’ •[ƒNƒŠƒA
        private void ClearDataGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns["Column1"].Visible = false;
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

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void KeihanAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxes(new[] {
            KeihanMainCheckBox, NakanoshimaCheckBox, UjiCheckBox,
            IshiyamasakamotoCheckBox, KeihanKousakuCheckBox,
            KeishinCheckBox, KatanoCheckBox
            }, KeihanAllCheckBox.Checked);
        }

        private void HankyuAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxes(new[] {
            KyotoCheckBox, KobeCheckBox, TakarazukaCheckBox, SenriCheckBox,
            ArashiyamaCheckBox, ItamiCheckBox, ImazuCheckBox,
            KoyoCheckBox, MinoCheckBox
            }, HankyuAllCheckBox.Checked);
        }

        private void HanshinAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxes(new[] {
            HanshinMainCheckBox, HanshinNambaCheckBox, MukogawaCheckBox
            }, HanshinAllCheckBox.Checked);
        }

        private void NankaiAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxes(new[] {
            NankaiMainCheckBox, KoyaCheckBox, ShiomibashiCheckBox, KukoCheckBox,
            KadaCheckBox, TakashinohamaCheckBox, TanagawaCheckBox, NankaiKosakuCheckBox
            }, NankaiAllCheckBox.Checked);
        }

        private void KintetsuAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckBoxes(new[] {
            OsakaCheckBox, KintetsuNambaCheckBox, KintetsuKyotoCheckBox,
            NaraCheckBox, MinamiOsakaCheckBox, KashiharaCheckBox,
            KeihannaCheckBox, IkomaCheckBox, IkomaKosakuCheckBox,
            NishishigiKosakuCheckBox, YoshinoCheckBox, NaganoCheckBox,
            ShigiCheckBox, DomyojiCheckBox, TawaramotoCheckBox,
            GoseCheckBox, TenriCheckBox, NagoyaCheckBox,
            YunoyamaCheckBox, SuzukaCheckBox, YamadaCheckBox,
            TobaCheckBox, ShimaCheckBox
            }, KintetsuAllCheckBox.Checked);
        }

        private void SetCheckBoxes(CheckBox[] targetCheckBoxes, bool isChecked)
        {
            foreach (var cb in targetCheckBoxes)
            {
                cb.Checked = isChecked;
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
                        else if (distance >= 6.0 && distance <= 6.9)
                        {
                            row.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                        else if (distance >= 7.0 && distance <= 7.9)
                        {
                            row.DefaultCellStyle.BackColor = Color.Tomato;
                        }
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
                            if (row.IsNewRow)
                            { 
                                continue; 
                            }

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@company", row.Cells["“S“¹‰ïĞ"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@line", row.Cells["˜Hü–¼"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@section", row.Cells["‹æŠÔ"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@distance", Convert.ToDouble(row.Cells["‰wŠÔ‹——£ (km)"].Value));

                            cmd.ExecuteNonQuery();
                        }
                        // šš³í‚É“o˜^@I‚í‚èšš

                        // ššƒ[ƒ‹ƒoƒbƒNŠm”F—pƒR[ƒhšš
                        // d•¡‚·‚éId‚ğINSERT‚·‚é
                        //cmd.CommandText = "INSERT INTO TrainSections (Id, Company, Line, Section, Distance) VALUES (235, 'JR', 'Yamanote', 'Shinjuku - Ikebukuro', 5.0)";
                        //cmd.ExecuteNonQuery();  // ‚±‚±‚Å—áŠO‚ª”­¶
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