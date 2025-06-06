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

        // 鉄道会社ごとの全件数（定数）
        private readonly Dictionary<string, int> totalCounts = new()
        {
            ["京阪"] = 86,
            ["近鉄"] = 285,
            ["阪急"] = 90,
            ["阪神"] = 45,
            ["南海"] = 100,
        };
        public Form1(Form2ViewModel viewModel)
        {
            InitializeComponent();
            _form2ViewModel = viewModel;


            // 駅間距離CSVファイルパスとチェックボックスの対応
            checkBoxToPath = new Dictionary<CheckBox, string>
            {
             { KeihanMainCheckBox, "..\\..\\..\\..\\駅間距離\\京阪\\京阪本線.csv" },
             { NakanoshimaCheckBox, "..\\..\\..\\..\\駅間距離\\京阪\\京阪中之島線.csv" },
             { UjiCheckBox, "..\\..\\..\\..\\駅間距離\\京阪\\京阪宇治線.csv" },
             { KeishinCheckBox, "..\\..\\..\\..\\駅間距離\\京阪\\京阪京津線.csv" },
             { KatanoCheckBox, "..\\..\\..\\..\\駅間距離\\京阪\\京阪交野線.csv" },
             { KeihanKousakuCheckBox, "..\\..\\..\\..\\駅間距離\\京阪\\京阪鋼索線.csv" },
             { IshiyamasakamotoCheckBox, "..\\..\\..\\..\\駅間距離\\京阪\\京阪石山坂本線.csv" },

             { KeihannaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄けいはんな線.csv" },
             { KashiharaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄橿原線.csv" },
             { YoshinoCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄吉野線.csv" },
             { KintetsuKyotoCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄京都線.csv" },
             { GoseCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄御所線.csv" },
             { YamadaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄山田線.csv" },
             { ShimaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄志摩線.csv" },
             { ShigiCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄信貴線.csv" },
             { IkomaKosakuCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄生駒鋼索線.csv" },
             { IkomaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄生駒線.csv" },
             { NishishigiKosakuCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄西信貴鋼索線.csv" },
             { OsakaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄大阪線.csv" },
             { NaganoCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄長野線.csv" },
             { TobaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄鳥羽線.csv" },
             { TenriCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄天理線.csv" },
             { TawaramotoCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄田原本線.csv" },
             { YunoyamaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄湯の山線.csv" },
             { DomyojiCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄道明寺線.csv" },
             { NaraCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄奈良線.csv" },
             { MinamiOsakaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄南大阪線.csv" },
             { KintetsuNambaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄難波線.csv" },
             { NagoyaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄名古屋線.csv" },
             { SuzukaCheckBox, "..\\..\\..\\..\\駅間距離\\近鉄\\近鉄鈴鹿線.csv" },

             { ItamiCheckBox, "..\\..\\..\\..\\駅間距離\\阪急\\阪急伊丹線.csv" },
             { KyotoCheckBox, "..\\..\\..\\..\\駅間距離\\阪急\\阪急京都線.csv" },
             { KoyoCheckBox, "..\\..\\..\\..\\駅間距離\\阪急\\阪急甲陽線.csv" },
             { ImazuCheckBox, "..\\..\\..\\..\\駅間距離\\阪急\\阪急今津線.csv" },
             { KobeCheckBox, "..\\..\\..\\..\\駅間距離\\阪急\\阪急神戸線.csv" },
             { SenriCheckBox, "..\\..\\..\\..\\駅間距離\\阪急\\阪急千里線.csv" },
             { TakarazukaCheckBox, "..\\..\\..\\..\\駅間距離\\阪急\\阪急宝塚線.csv" },
             { MinoCheckBox, "..\\..\\..\\..\\駅間距離\\阪急\\阪急箕面線.csv" },
             { ArashiyamaCheckBox, "..\\..\\..\\..\\駅間距離\\阪急\\阪急嵐山線.csv" },

             { HanshinNambaCheckBox, "..\\..\\..\\..\\駅間距離\\阪神\\阪神なんば線.csv" },
             { MukogawaCheckBox, "..\\..\\..\\..\\駅間距離\\阪神\\阪神武庫川線.csv" },
             { HanshinMainCheckBox, "..\\..\\..\\..\\駅間距離\\阪神\\阪神本線.csv" },

             { KadaCheckBox, "..\\..\\..\\..\\駅間距離\\南海\\南海加太線.csv" },
             { KukoCheckBox, "..\\..\\..\\..\\駅間距離\\南海\\南海空港線.csv" },
             { NankaiKosakuCheckBox, "..\\..\\..\\..\\駅間距離\\南海\\南海鋼索線.csv" },
             { TakashinohamaCheckBox, "..\\..\\..\\..\\駅間距離\\南海\\南海高師浜線.csv" },
             { KoyaCheckBox, "..\\..\\..\\..\\駅間距離\\南海\\南海高野線.csv" },
             { ShiomibashiCheckBox, "..\\..\\..\\..\\駅間距離\\南海\\南海汐見橋線.csv" },
             { TanagawaCheckBox, "..\\..\\..\\..\\駅間距離\\南海\\南海多奈川線.csv" },
             { NankaiMainCheckBox, "..\\..\\..\\..\\駅間距離\\南海\\南海本線.csv" },
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
                MessageBox.Show("検索結果は0件です。");
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

        // CSV選択ロジック
        private List<string> GetSelectedCsvPaths()
        {
            return checkBoxToPath
                .Where(kvp => kvp.Key.Checked)
                .Select(kvp => kvp.Value)
                .ToList();
        }

        // データ読み込みとマージ
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

        // 2つのDataTableをマージするメソッド
        private DataTable MergeDataTables(DataTable table1, DataTable table2)
        {
            DataTable mergedTable = table1.Copy();

            // 2つ目のテーブルの行を追加
            foreach (DataRow row in table2.Rows)
            {
                mergedTable.ImportRow(row);
            }

            return mergedTable;
        }

        // フィルタ処理
        private DataTable ApplyDistanceFilter(DataTable data)
        {
            if (!double.TryParse(textBoxFom1Min.Text, out double min) ||
                !double.TryParse(textBoxFom1Max.Text, out double max))
            {
                return data;
            }

            var filtered = data.AsEnumerable()
                .Where(row => double.TryParse(row["駅間距離 (km)"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value)
                              && value >= min && value <= max);

            return filtered.Any() ? filtered.CopyToDataTable() : data.Clone();
        }

        // データ表示更新
        private void UpdateDataGrid(DataTable data)
        {
            dataGridView1.DataSource = data;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["Column1"].Value = i + 1;
            }

            dataGridView1.Columns["Column1"].Width = 50;
        }

        // 件数ラベル表示更新
        private void UpdateRailwayCountLabels(DataTable data)
        {
            var grouped = data.AsEnumerable()
                .GroupBy(row => row.Field<string>("鉄道会社"))
                .ToDictionary(g => g.Key, g => g.Count());

            labelKeihanCount.Text = FormatLabel("京阪", grouped);
            labelKintetsuCount.Text = FormatLabel("近鉄", grouped);
            labelHankyuCount.Text = FormatLabel("阪急", grouped);
            labelHanshinCount.Text = FormatLabel("阪神", grouped);
            labelNankaiCount.Text = FormatLabel("南海", grouped);
        }

        private string FormatLabel(string company, Dictionary<string, int> grouped)
        {
            int count = grouped.ContainsKey(company) ? grouped[company] : 0;
            int total = totalCounts[company];
            string percent = total > 0 ? $" ({(count * 100.0 / total):F1}%)" : "";
            return $"{company}: {count} 件 / {total}件{percent}";
        }

        // 帳票クリア
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

                        // ヘッダーを読み込む（最初の行）
                        if (!headerRead)
                        {
                            dataGridView1.Columns["Column1"].Visible = true;
                            dataTable.Columns.Add("鉄道会社");
                            dataTable.Columns.Add("路線名");
                            dataTable.Columns.Add("区間");
                            dataTable.Columns.Add("駅間距離 (km)");
                            headerRead = true;
                        }

                        // 最初の駅のデータを保存
                        string company = values[0].Trim();
                        string lineName = values[1].Trim();
                        string stationName = values[2].Trim();

                        // 数値の解析を文化に依存しないように変更
                        double distance = double.Parse(values[3].Trim(), CultureInfo.InvariantCulture);  // InvariantCultureを使用

                        // 最初の駅と次の駅のペアを作成
                        if (!string.IsNullOrEmpty(previousStation))
                        {
                            string stationPair = $"{previousStation}ー{stationName}";
                            dataTable.Rows.Add(previousCompany, previousLine, stationPair, distance);
                        }

                        // 現在の駅の情報を次回のループで使用するために保存
                        previousStation = stationName;
                        previousCompany = company;
                        previousLine = lineName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CSV読み込みエラー: {ex.Message}");
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
                MessageBox.Show("エクスポートするデータがありません。");
                return;
            }

            try
            {
                using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    // ヘッダー行
                    var headers = dataGridView1.Columns
                        .Cast<DataGridViewColumn>()
                        .Where(c => c.Visible)
                        .Select(c => c.HeaderText)
                        .ToArray();
                    writer.WriteLine(string.Join(",", headers));

                    // 各データ行
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

                MessageBox.Show("CSVファイルのエクスポートが完了しました。");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CSVのエクスポート中にエラーが発生しました: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_form2 == null || _form2.IsDisposed)
            {
                _form2 = new Form2(_form2ViewModel);
            }
            // モーダル表示
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
                        case "0.0〜0.9":
                            textBoxFom1Min.Text = "0.0";
                            textBoxFom1Max.Text = "0.9";
                            break;
                        case "1.0〜1.9":
                            textBoxFom1Min.Text = "1.0";
                            textBoxFom1Max.Text = "1.9";
                            break;
                        case "2.0〜2.9":
                            textBoxFom1Min.Text = "2.0";
                            textBoxFom1Max.Text = "2.9";
                            break;
                        case "3.0〜3.9":
                            textBoxFom1Min.Text = "3.0";
                            textBoxFom1Max.Text = "3.9";
                            break;
                        case "4.0〜4.9":
                            textBoxFom1Min.Text = "4.0";
                            textBoxFom1Max.Text = "4.9";
                            break;
                        case "5.0〜5.9":
                            textBoxFom1Min.Text = "5.0";
                            textBoxFom1Max.Text = "5.9";
                            break;
                        case "6.0〜6.9":
                            textBoxFom1Min.Text = "6.0";
                            textBoxFom1Max.Text = "6.9";
                            break;
                        case "7.0〜7.9":
                            textBoxFom1Min.Text = "7.0";
                            textBoxFom1Max.Text = "7.9";
                            break;
                    }
                }
            }
        }

        private void Coloring()
        {
            // 駅間距離に応じて行に色をつける処理
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (double.TryParse(row.Cells["駅間距離 (km)"].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double distance))
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
                saveFileDialog.Filter = "CSVファイル (*.csv)|*.csv";
                saveFileDialog.Title = "CSVファイルの保存";
                saveFileDialog.FileName = "出力結果.csv";

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
                MessageBox.Show("保存するデータがありません。");
                return;
            }

            // DBとテーブルを準備
            EnsureDatabase();

            using (var conn = new SQLiteConnection(_dbPath))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        // ★★正常に登録★★
                        cmd.CommandText = "INSERT INTO TrainSections (Company, Line, Section, Distance) VALUES (@company, @line, @section, @distance)";

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsNewRow)
                            { 
                                continue; 
                            }

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@company", row.Cells["鉄道会社"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@line", row.Cells["路線名"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@section", row.Cells["区間"].Value?.ToString());
                            cmd.Parameters.AddWithValue("@distance", Convert.ToDouble(row.Cells["駅間距離 (km)"].Value));

                            cmd.ExecuteNonQuery();
                        }
                        // ★★正常に登録　終わり★★

                        // ★★ロールバック確認用コード★★
                        // 重複するIdをINSERTする
                        //cmd.CommandText = "INSERT INTO TrainSections (Id, Company, Line, Section, Distance) VALUES (235, 'JR', 'Yamanote', 'Shinjuku - Ikebukuro', 5.0)";
                        //cmd.ExecuteNonQuery();  // ここで例外が発生
                        // ★★ロールバック確認用コード　終わり★★
                    }

                    transaction.Commit();
                }
            }

            MessageBox.Show("データベースへの登録が完了しました。");
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            SaveDataToDatabase();
        }
    }
}