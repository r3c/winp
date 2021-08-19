using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winp.Configuration;

namespace Winp
{
    internal class Package
    {
        public readonly IInstallable Installable;
        public readonly Instance? Instance;

        public PackageVariantConfig? Variant { get; private set; }

        private readonly Label _statusLabel;
        private readonly TaskScheduler _scheduler;
        private readonly Action _variantChange;
        private readonly ComboBox _variantComboBox;

        public Package(IInstallable installable, Instance? instance, TaskScheduler scheduler, ComboBox variantComboBox, Label statusLabel, IReadOnlyList<PackageVariantConfig> variants, Action<Package> onVariantChange)
        {
            var variantChange = new Action(() =>
            {
                Variant = variantComboBox.SelectedItem is PackageVariantConfig variant ? variant : null;

                onVariantChange(this);
            });

            variantComboBox.Items.AddRange(variants.ToArray());

            if (variants.Count > 0)
                variantComboBox.SelectedIndex = 0;

            variantComboBox.SelectedIndexChanged += new EventHandler((source, args) => variantChange());

            _scheduler = scheduler;
            _statusLabel = statusLabel;
            _variantChange = variantChange;
            _variantComboBox = variantComboBox;

            Installable = installable;
            Instance = instance;
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

                _statusLabel.ImageIndex = (int)(object)statusIndex; // That's ugly, but not a big deal perf-wise as we're doing GUI updates anyway
                _statusLabel.Text = text;

                _statusLabel.AutoSize = true;

                var width = _statusLabel.Width;

                _statusLabel.AutoSize = false;

                _statusLabel.Width = _statusLabel.ImageList.ImageSize.Width + space + width;
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
}