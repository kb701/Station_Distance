using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form2 : Form
    {
        public Form2ViewModel ViewModel { get; private set; } = new Form2ViewModel();

        public string MinValue => ViewModel.MinValue;
        public string MaxValue => ViewModel.MaxValue;

        public Form2(Form2ViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;

            // ★★★★★バインディング★★★★★
            // バインディング（WinForms では手動で）
            textBoxForm2Min.DataBindings.Add("Text", ViewModel, nameof(ViewModel.MinValue), false, DataSourceUpdateMode.OnPropertyChanged);
            textBoxForm2Max.DataBindings.Add("Text", ViewModel, nameof(ViewModel.MaxValue), false, DataSourceUpdateMode.OnPropertyChanged);

            // コンボボックスのアイテムを ViewModel の Options にバインディング
            comboBox1.DataSource = ViewModel.Options;

            // コンボボックスの選択項目（SelectedOption）を ViewModel にバインディング
            comboBox1.DataBindings.Add("SelectedItem", ViewModel, "SelectedOption", false, DataSourceUpdateMode.OnPropertyChanged);

            // OKボタンにコマンドをバインディング
            OKButton.Click += (s, e) =>
            {
                // OKCommand を実行
                ViewModel.OKButtonClickedCommand.Execute(null);
            };

            // リセットボタンにコマンドをバインディング
            ResetButton.Click += (s, e) =>
            {
                // ResetCommand を実行
                ViewModel.ResetCommand.Execute(null);
            };

            // ヘルプボタンにコマンドをバインディング
            HelpButton.Click += (s, e) =>
            {
                // ShowHelpCommand を実行
                ViewModel.ShowHelpCommand.Execute(null);
            };


            // ViewModel の CloseFormRequested イベントを購読
            ViewModel.CloseFormRequested += () =>
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
            };
        }

        // ★★★★★操作をしたときのＵＩの変化★★★★★
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ViewModel.IsManualInputEnabled = radioButton1.Checked;
            textBoxForm2Min.Enabled = ViewModel.IsManualInputEnabled;
            textBoxForm2Max.Enabled = ViewModel.IsManualInputEnabled;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ViewModel.IsAutoInputEnabled = radioButton2.Checked;
            comboBox1.Enabled = radioButton2.Checked;
        }
    }
}
