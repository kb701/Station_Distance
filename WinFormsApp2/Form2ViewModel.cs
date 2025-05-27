using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

public partial class Form2ViewModel : ObservableObject
{
    // ★★★★★ ViewでBindingした変数 ★★★★★
    [ObservableProperty]
    private string minValue;

    [ObservableProperty]
    private string maxValue;

    [ObservableProperty]
    private bool isManualInputEnabled = true;

    [ObservableProperty]
    private bool isAutoInputEnabled = false;

    [ObservableProperty]
    private List<string> options = new() { "0.0～0.9", "1.0～1.9", "2.0～2.9", "3.0～3.9", "4.0～4.9", "5.0～5.9", "6.0～6.9", "7.0～7.9" };

    [ObservableProperty]
    private string selectedOption = "0.0～0.9";

    // ★★★★★ イベントの定義 ★★★★★
    public event Action CloseFormRequested;

    // ★★★★★ RelayCommand を定義 ★★★★★
    [RelayCommand]
    private void OnOKButtonClicked()
    {
        if (IsManualInputEnabled)
        {
            if (string.IsNullOrWhiteSpace(MinValue) || string.IsNullOrWhiteSpace(MaxValue))
            {
                MessageBox.Show("最小値と最大値を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!double.TryParse(MinValue, out double min) || !double.TryParse(MaxValue, out double max))
            {
                MessageBox.Show("最小値と最大値は有効な数値でなければなりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (min > max)
            {
                MessageBox.Show("最小値は最大値より小さくなければなりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        else
        {
            // 自動入力モードの処理があればここに
        }

        CloseFormRequested?.Invoke();
    }

    [RelayCommand]
    private void Reset()
    {
        MinValue = string.Empty;
        MaxValue = string.Empty;
        SelectedOption = Options.Count > 0 ? Options[0] : string.Empty;

        MessageBox.Show("入力がリセットされました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    [RelayCommand]
    private void ShowHelp()
    {
        MessageBox.Show(
            "・手動入力の場合は最小値と最大値を入力してください。\n" +
            "・自動入力の場合はコンボボックスから範囲を選択してください。\n\n" +
            "選択された範囲は帳票作成などに利用されます。",
            "ヘルプ",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
    }
}