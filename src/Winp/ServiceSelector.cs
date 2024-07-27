using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winp.Configuration;

namespace Winp;

internal class ServiceSelector
{
    public readonly IPackage Package;
    public readonly ServiceRunner? Runner;

    public PackageVariantConfig? Variant { get; private set; }

    private readonly Label _statusLabel;
    private readonly TaskScheduler _scheduler;
    private readonly Action _variantChange;
    private readonly ComboBox _variantComboBox;

    public ServiceSelector(IPackage package, ServiceRunner? runner, TaskScheduler scheduler,
        ComboBox variantComboBox, Label statusLabel, IReadOnlyList<PackageVariantConfig> variants,
        Action<ServiceSelector> onVariantChange)
    {
        var variantChange = new Action(() =>
        {
            Variant = variantComboBox.SelectedItem as PackageVariantConfig;

            onVariantChange(this);
        });

        foreach (var variant in variants)
            variantComboBox.Items.Add(variant);

        if (variants.Count > 0)
            variantComboBox.SelectedIndex = 0;

        variantComboBox.SelectedIndexChanged += (_, _) => variantChange();

        _scheduler = scheduler;
        _statusLabel = statusLabel;
        _variantChange = variantChange;
        _variantComboBox = variantComboBox;

        Package = package;
        Runner = runner;
    }

    public void Initialize()
    {
        _variantChange();
    }

    /// <summary>
    /// Set text and left-aligned icon on underlying label component.
    /// </summary>
    public void SetText<T>(T statusIndex, string text) where T : Enum
    {
        ExecuteGuiAction(() =>
        {
            const int space = 4;

            _statusLabel.ImageIndex =
                (int)(object)statusIndex; // That's ugly, but not a big deal perf-wise as we're doing GUI updates anyway
            _statusLabel.Text = text;

            _statusLabel.AutoSize = true;

            var imageWidth = _statusLabel.ImageList is not null ? _statusLabel.ImageList.ImageSize.Width : 0;
            var labelWidth = _statusLabel.Width;

            _statusLabel.AutoSize = false;
            _statusLabel.Width = imageWidth + space + labelWidth;
        });
    }

    public void VersionLock()
    {
        ExecuteGuiAction(() => _variantComboBox.Enabled = false);
    }

    public void VersionUnlock()
    {
        ExecuteGuiAction(() => _variantComboBox.Enabled = true);
    }

    private void ExecuteGuiAction(Action action)
    {
        var task = new Task(action);

        task.Start(_scheduler);
    }
}