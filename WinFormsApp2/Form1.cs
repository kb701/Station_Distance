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
            // CSV�t�@�C���̃p�X�i5��CSV�t�@�C���j
            string[] csvFilePaths = new string[]
            {
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\����\\����{��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\����\\���㒆�V����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\����\\����F����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\����\\���㋞�Ð�.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\����\\�������.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\����\\����|����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\����\\����ΎR��{��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�����͂�Ȑ�.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS������.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�g���.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS���s��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�䏊��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�R�c��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�u����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�M�M��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS����|����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS���M�M�|����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS���H��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�V����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�c���{��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS���̎R��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS��������.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�ޗǐ�.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS��g��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS���É���.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\�ߓS\\�ߓS�鎭��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��}\\��}�ɒO��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��}\\��}���s��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��}\\��}�b�z��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��}\\��}���Ð�.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��}\\��}�_�ː�.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��}\\��}�痢��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��}\\��}��ː�.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��}\\��}���ʐ�.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��}\\��}���R��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��_\\��_�Ȃ�ΐ�.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��_\\��_���ɐ��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��_\\��_�{��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��C\\��C������.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��C\\��C��`��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��C\\��C�|����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��C\\��C���t�l��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��C\\��C�����.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��C\\��C��������.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��C\\��C���ސ��.csv",
            "C:\\GitHub\\StationDistance\\�w�ԋ���\\��C\\��C�{��.csv",
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

            // �ŏ���CSV�t�@�C���̃f�[�^��ǂݍ���
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
                    // �c���CSV�t�@�C����ǂݍ���Ń}�[�W����
                    for (int i = 1; i < validcsvFilePaths.Count; i++)
                    {
                        var csvData = ReadCsv(validcsvFilePaths[i]);
                        mergedData = MergeDataTables(mergedData, csvData);
                    }
                }

                // �����Ńt�B���^����
                if (double.TryParse(textBoxFom1Min.Text, out double minValue) &&
                    double.TryParse(textBoxFom1Max.Text, out double maxValue))
                {
                    var filteredRows = mergedData.AsEnumerable()
                        .Where(row => double.TryParse(row["�w�ԋ��� (km)"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value) &&
                                      value >= minValue && value <= maxValue);

                    if (filteredRows.Any())
                    {
                        mergedData = filteredRows.CopyToDataTable();
                    }
                    else
                    {
                        mergedData.Rows.Clear();
                        MessageBox.Show("�������ʂ�0���ł��B");
                    }
                }

                // DataGridView�Ƀf�[�^��ݒ�
                dataGridView1.DataSource = mergedData;

                // �S����ЕʂɌ������W�v
                var groupedCounts = mergedData.AsEnumerable()
                    .GroupBy(row => row.Field<string>("�S�����"))
                    .ToDictionary(g => g.Key, g => g.Count());

                // �e���x���Ɍ����𔽉f�i�Y��������΁j
                if (groupedCounts.ContainsKey("����"))
                {
                    string percentage = 86 > 0 ? $" ({(groupedCounts["����"] * 100.0 / 86):F1}%)" : "";
                    labelKeihanCount.Text = $"����: {groupedCounts["����"]} �� / 86��{percentage}";
                }
                else
                {
                    labelKeihanCount.Text = $"����: 0 �� / 86�� (0%)";
                }

                if (groupedCounts.ContainsKey("�ߓS"))
                {
                    string percentage = 285 > 0 ? $" ({(groupedCounts["�ߓS"] * 100.0 / 285):F1}%)" : "";
                    labelKintetsuCount.Text = $"�ߓS: {groupedCounts["�ߓS"]} �� / 285��{percentage}";
                }
                else
                {
                    labelKintetsuCount.Text = $"�ߓS: 0 �� / 285�� (0%)";
                }

                if (groupedCounts.ContainsKey("��}"))
                {
                    string percentage = 90 > 0 ? $" ({(groupedCounts["��}"] * 100.0 / 90):F1}%)" : "";
                    labelHankyuCount.Text = $"��}: {groupedCounts["��}"]} �� / 90��{percentage}";
                }
                else
                {
                    labelHankyuCount.Text = $"��}: 0 �� / 90�� (0%)";
                }

                if (groupedCounts.ContainsKey("��_"))
                {
                    string percentage = 45 > 0 ? $" ({(groupedCounts["��_"] * 100.0 / 45):F1}%)" : "";
                    labelHanshinCount.Text = $"��_: {groupedCounts["��_"]} ��/ 45��{percentage} ";
                }
                else
                {
                    labelHanshinCount.Text = $"��_: 0 �� / 45�� (0%)";
                }

                if (groupedCounts.ContainsKey("��C"))
                {
                    string percentage = 100 > 0 ? $" ({(groupedCounts["��C"] * 100.0 / 100):F1}%)" : "";
                    labelNankaiCount.Text = $"��C: {groupedCounts["��C"]} �� / 100��{percentage}";
                }
                else
                {
                    labelNankaiCount.Text = $"��C: 0 �� / 100�� (0%)";
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
                        // �K�v�ɉ����Ēǉ�
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
                            if (row.IsNewRow) continue;

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@company", row.Cells["�S�����"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@line", row.Cells["�H����"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@section", row.Cells["���"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@distance", Convert.ToDouble(row.Cells["�w�ԋ��� (km)"].Value));

                            cmd.ExecuteNonQuery();
                        }
                        // ��������ɓo�^�@�I��聚��

                        // �������[���o�b�N�m�F�p�R�[�h����
                        // �@ �s���ȃf�[�^�iDistance��ɕ���������ăG���[���N�����j
                        cmd.CommandText = "INSERT INTO TrainSections (Id, Company, Line, Section, Distance) VALUES (235, 'JR', 'Yamanote', 'Shinjuku - Ikebukuro', 5.0)";
                        cmd.ExecuteNonQuery();  // �����ŗ�O������
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