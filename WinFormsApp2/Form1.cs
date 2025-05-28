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

        // �S����Ђ��Ƃ̑S�����i�萔�j
        private readonly Dictionary<string, int> totalCounts = new()
        {
            ["����"] = 86,
            ["�ߓS"] = 285,
            ["��}"] = 90,
            ["��_"] = 45,
            ["��C"] = 100,
        };
        public Form1(Form2ViewModel viewModel)
        {
            InitializeComponent();
            _form2ViewModel = viewModel;


            // �w�ԋ���CSV�t�@�C���p�X�ƃ`�F�b�N�{�b�N�X�̑Ή�
            checkBoxToPath = new Dictionary<CheckBox, string>
            {
             { KeihanMainCheckBox, "..\\..\\..\\..\\�w�ԋ���\\����\\����{��.csv" },
             { NakanoshimaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\����\\���㒆�V����.csv" },
             { UjiCheckBox, "..\\..\\..\\..\\�w�ԋ���\\����\\����F����.csv" },
             { KeishinCheckBox, "..\\..\\..\\..\\�w�ԋ���\\����\\���㋞�Ð�.csv" },
             { KatanoCheckBox, "..\\..\\..\\..\\�w�ԋ���\\����\\�������.csv" },
             { KeihanKousakuCheckBox, "..\\..\\..\\..\\�w�ԋ���\\����\\����|����.csv" },
             { IshiyamasakamotoCheckBox, "..\\..\\..\\..\\�w�ԋ���\\����\\����ΎR��{��.csv" },

             { KeihannaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�����͂�Ȑ�.csv" },
             { KashiharaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS������.csv" },
             { YoshinoCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�g���.csv" },
             { KintetsuKyotoCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS���s��.csv" },
             { GoseCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�䏊��.csv" },
             { YamadaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�R�c��.csv" },
             { ShimaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�u����.csv" },
             { ShigiCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�M�M��.csv" },
             { IkomaKosakuCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS����|����.csv" },
             { IkomaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�����.csv" },
             { NishishigiKosakuCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS���M�M�|����.csv" },
             { OsakaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS����.csv" },
             { NaganoCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�����.csv" },
             { TobaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS���H��.csv" },
             { TenriCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�V����.csv" },
             { TawaramotoCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�c���{��.csv" },
             { YunoyamaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS���̎R��.csv" },
             { DomyojiCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS��������.csv" },
             { NaraCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�ޗǐ�.csv" },
             { MinamiOsakaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�����.csv" },
             { KintetsuNambaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS��g��.csv" },
             { NagoyaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS���É���.csv" },
             { SuzukaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\�ߓS\\�ߓS�鎭��.csv" },

             { ItamiCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��}\\��}�ɒO��.csv" },
             { KyotoCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��}\\��}���s��.csv" },
             { KoyoCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��}\\��}�b�z��.csv" },
             { ImazuCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��}\\��}���Ð�.csv" },
             { KobeCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��}\\��}�_�ː�.csv" },
             { SenriCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��}\\��}�痢��.csv" },
             { TakarazukaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��}\\��}��ː�.csv" },
             { MinoCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��}\\��}���ʐ�.csv" },
             { ArashiyamaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��}\\��}���R��.csv" },

             { HanshinNambaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��_\\��_�Ȃ�ΐ�.csv" },
             { MukogawaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��_\\��_���ɐ��.csv" },
             { HanshinMainCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��_\\��_�{��.csv" },

             { KadaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��C\\��C������.csv" },
             { KukoCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��C\\��C��`��.csv" },
             { NankaiKosakuCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��C\\��C�|����.csv" },
             { TakashinohamaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��C\\��C���t�l��.csv" },
             { KoyaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��C\\��C�����.csv" },
             { ShiomibashiCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��C\\��C��������.csv" },
             { TanagawaCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��C\\��C���ސ��.csv" },
             { NankaiMainCheckBox, "..\\..\\..\\..\\�w�ԋ���\\��C\\��C�{��.csv" },
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
                MessageBox.Show("�������ʂ�0���ł��B");
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

        // CSV�I�����W�b�N
        private List<string> GetSelectedCsvPaths()
        {
            return checkBoxToPath
                .Where(kvp => kvp.Key.Checked)
                .Select(kvp => kvp.Value)
                .ToList();
        }

        // �f�[�^�ǂݍ��݂ƃ}�[�W
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

        // 2��DataTable���}�[�W���郁�\�b�h
        private DataTable MergeDataTables(DataTable table1, DataTable table2)
        {
            DataTable mergedTable = table1.Copy();

            // 2�ڂ̃e�[�u���̍s��ǉ�
            foreach (DataRow row in table2.Rows)
            {
                mergedTable.ImportRow(row);
            }

            return mergedTable;
        }

        // �t�B���^����
        private DataTable ApplyDistanceFilter(DataTable data)
        {
            if (!double.TryParse(textBoxFom1Min.Text, out double min) ||
                !double.TryParse(textBoxFom1Max.Text, out double max))
            {
                return data;
            }

            var filtered = data.AsEnumerable()
                .Where(row => double.TryParse(row["�w�ԋ��� (km)"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value)
                              && value >= min && value <= max);

            return filtered.Any() ? filtered.CopyToDataTable() : data.Clone();
        }

        // �f�[�^�\���X�V
        private void UpdateDataGrid(DataTable data)
        {
            dataGridView1.DataSource = data;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["Column1"].Value = i + 1;
            }

            dataGridView1.Columns["Column1"].Width = 50;
        }

        // �������x���\���X�V
        private void UpdateRailwayCountLabels(DataTable data)
        {
            var grouped = data.AsEnumerable()
                .GroupBy(row => row.Field<string>("�S�����"))
                .ToDictionary(g => g.Key, g => g.Count());

            labelKeihanCount.Text = FormatLabel("����", grouped);
            labelKintetsuCount.Text = FormatLabel("�ߓS", grouped);
            labelHankyuCount.Text = FormatLabel("��}", grouped);
            labelHanshinCount.Text = FormatLabel("��_", grouped);
            labelNankaiCount.Text = FormatLabel("��C", grouped);
        }

        private string FormatLabel(string company, Dictionary<string, int> grouped)
        {
            int count = grouped.ContainsKey(company) ? grouped[company] : 0;
            int total = totalCounts[company];
            string percent = total > 0 ? $" ({(count * 100.0 / total):F1}%)" : "";
            return $"{company}: {count} �� / {total}��{percent}";
        }

        // ���[�N���A
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

                        // �w�b�_�[��ǂݍ��ށi�ŏ��̍s�j
                        if (!headerRead)
                        {
                            dataGridView1.Columns["Column1"].Visible = true;
                            dataTable.Columns.Add("�S�����");
                            dataTable.Columns.Add("�H����");
                            dataTable.Columns.Add("���");
                            dataTable.Columns.Add("�w�ԋ��� (km)");
                            headerRead = true;
                        }

                        // �ŏ��̉w�̃f�[�^��ۑ�
                        string company = values[0].Trim();
                        string lineName = values[1].Trim();
                        string stationName = values[2].Trim();

                        // ���l�̉�͂𕶉��Ɉˑ����Ȃ��悤�ɕύX
                        double distance = double.Parse(values[3].Trim(), CultureInfo.InvariantCulture);  // InvariantCulture���g�p

                        // �ŏ��̉w�Ǝ��̉w�̃y�A���쐬
                        if (!string.IsNullOrEmpty(previousStation))
                        {
                            string stationPair = $"{previousStation}�[{stationName}";
                            dataTable.Rows.Add(previousCompany, previousLine, stationPair, distance);
                        }

                        // ���݂̉w�̏�������̃��[�v�Ŏg�p���邽�߂ɕۑ�
                        previousStation = stationName;
                        previousCompany = company;
                        previousLine = lineName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CSV�ǂݍ��݃G���[: {ex.Message}");
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
                MessageBox.Show("�G�N�X�|�[�g����f�[�^������܂���B");
                return;
            }

            try
            {
                using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    // �w�b�_�[�s
                    var headers = dataGridView1.Columns
                        .Cast<DataGridViewColumn>()
                        .Where(c => c.Visible)
                        .Select(c => c.HeaderText)
                        .ToArray();
                    writer.WriteLine(string.Join(",", headers));

                    // �e�f�[�^�s
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

                MessageBox.Show("CSV�t�@�C���̃G�N�X�|�[�g���������܂����B");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CSV�̃G�N�X�|�[�g���ɃG���[���������܂���: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_form2 == null || _form2.IsDisposed)
            {
                _form2 = new Form2(_form2ViewModel);
            }
            // ���[�_���\��
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
                        case "0.0�`0.9":
                            textBoxFom1Min.Text = "0.0";
                            textBoxFom1Max.Text = "0.9";
                            break;
                        case "1.0�`1.9":
                            textBoxFom1Min.Text = "1.0";
                            textBoxFom1Max.Text = "1.9";
                            break;
                        case "2.0�`2.9":
                            textBoxFom1Min.Text = "2.0";
                            textBoxFom1Max.Text = "2.9";
                            break;
                        case "3.0�`3.9":
                            textBoxFom1Min.Text = "3.0";
                            textBoxFom1Max.Text = "3.9";
                            break;
                        case "4.0�`4.9":
                            textBoxFom1Min.Text = "4.0";
                            textBoxFom1Max.Text = "4.9";
                            break;
                        case "5.0�`5.9":
                            textBoxFom1Min.Text = "5.0";
                            textBoxFom1Max.Text = "5.9";
                            break;
                        case "6.0�`6.9":
                            textBoxFom1Min.Text = "6.0";
                            textBoxFom1Max.Text = "6.9";
                            break;
                        case "7.0�`7.9":
                            textBoxFom1Min.Text = "7.0";
                            textBoxFom1Max.Text = "7.9";
                            break;
                    }
                }
            }
        }

        private void Coloring()
        {
            // �w�ԋ����ɉ����čs�ɐF�����鏈��
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (double.TryParse(row.Cells["�w�ԋ��� (km)"].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double distance))
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
                saveFileDialog.Filter = "CSV�t�@�C�� (*.csv)|*.csv";
                saveFileDialog.Title = "CSV�t�@�C���̕ۑ�";
                saveFileDialog.FileName = "�o�͌���.csv";

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
                MessageBox.Show("�ۑ�����f�[�^������܂���B");
                return;
            }

            // DB�ƃe�[�u��������
            EnsureDatabase();

            using (var conn = new SQLiteConnection(_dbPath))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        // ��������ɓo�^����
                        cmd.CommandText = "INSERT INTO TrainSections (Company, Line, Section, Distance) VALUES (@company, @line, @section, @distance)";

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsNewRow)
                            { 
                                continue; 
                            }

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@company", row.Cells["�S�����"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@line", row.Cells["�H����"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@section", row.Cells["���"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@distance", Convert.ToDouble(row.Cells["�w�ԋ��� (km)"].Value));

                            cmd.ExecuteNonQuery();
                        }
                        // ��������ɓo�^�@�I��聚��

                        // �������[���o�b�N�m�F�p�R�[�h����
                        // �d������Id��INSERT����
                        //cmd.CommandText = "INSERT INTO TrainSections (Id, Company, Line, Section, Distance) VALUES (235, 'JR', 'Yamanote', 'Shinjuku - Ikebukuro', 5.0)";
                        //cmd.ExecuteNonQuery();  // �����ŗ�O������
                        // �������[���o�b�N�m�F�p�R�[�h�@�I��聚��
                    }

                    transaction.Commit();
                }
            }

            MessageBox.Show("�f�[�^�x�[�X�ւ̓o�^���������܂����B");
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            SaveDataToDatabase();
        }
    }
}