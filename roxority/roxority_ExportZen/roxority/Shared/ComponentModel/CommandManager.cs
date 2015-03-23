namespace roxority.Shared.ComponentModel
{
    using roxority.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Forms;

    [ProvideProperty("CommandKeyConsumed", typeof(ToolStripItem)), ProvideProperty("CommandKeyProvided", typeof(ToolStripItem))]
    internal class CommandManager : Component, IExtenderProvider
    {
        protected readonly EventHandler consumerItem_Click;
        private readonly Dictionary<ToolStripItem, string> consumers = new Dictionary<ToolStripItem, string>();
        internal static readonly Dictionary globalCommands = new Dictionary();
        internal static readonly List<CommandManager> globalInstances = new List<CommandManager>();
        private bool isGlobal;
        private readonly Dictionary localCommands = new Dictionary();
        protected readonly EventHandler providerItem_CheckedChanged;
        protected readonly EventHandler providerItem_EnabledChanged;
        protected readonly EventHandler providerItem_TextChanged;

        internal CommandManager()
        {
            this.consumerItem_Click = new EventHandler(this.ConsumerItem_Click);
            this.providerItem_EnabledChanged = new EventHandler(this.ProviderItem_EnabledChanged);
            this.providerItem_CheckedChanged = new EventHandler(this.ProviderItem_CheckedChanged);
            this.providerItem_TextChanged = new EventHandler(this.ProviderItem_TextChanged);
            globalInstances.Add(this);
        }

        protected virtual void ConsumerItem_Click(object sender, EventArgs e)
        {
            string str;
            ToolStripSplitButton button;
            ToolStripItem item;
            ToolStripItem commandItem = sender as ToolStripItem;
            if ((((button = commandItem as ToolStripSplitButton) == null) || !button.DropDownButtonPressed) && (((commandItem != null) && !SharedUtil.IsEmpty(str = this.GetCommandKeyConsumed(commandItem))) && ((item = this[str]) != null)))
            {
                item.PerformClick();
            }
        }

        protected override void Dispose(bool disposing)
        {
            globalInstances.Remove(this);
            base.Dispose(disposing);
        }

        [Localizable(false), DefaultValue(""), ExtenderProvidedProperty]
        internal string GetCommandKeyConsumed(ToolStripItem commandItem)
        {
            string str;
            if (!this.consumers.TryGetValue(commandItem, out str))
            {
                return string.Empty;
            }
            return str;
        }

        [DefaultValue(""), ExtenderProvidedProperty, Localizable(false)]
        internal string GetCommandKeyProvided(ToolStripItem commandItem)
        {
            return this[commandItem];
        }

        protected virtual void ProviderItem_CheckedChanged(object sender, EventArgs e)
        {
            string str;
            ToolStripItem providerToolStripItem = sender as ToolStripItem;
            if ((providerToolStripItem != null) && !SharedUtil.IsEmpty(str = this[providerToolStripItem]))
            {
                this.RefreshConsumers(providerToolStripItem, str);
            }
            if (e != null)
            {
                foreach (CommandManager manager in globalInstances)
                {
                    if ((manager != this) && manager.isGlobal)
                    {
                        manager.ProviderItem_CheckedChanged(sender, null);
                    }
                }
            }
        }

        protected virtual void ProviderItem_EnabledChanged(object sender, EventArgs e)
        {
            string str;
            ToolStripItem providerToolStripItem = sender as ToolStripItem;
            if ((providerToolStripItem != null) && !SharedUtil.IsEmpty(str = this[providerToolStripItem]))
            {
                this.RefreshConsumers(providerToolStripItem, str);
            }
            if (e != null)
            {
                foreach (CommandManager manager in globalInstances)
                {
                    if ((manager != this) && manager.isGlobal)
                    {
                        manager.ProviderItem_EnabledChanged(sender, null);
                    }
                }
            }
        }

        protected virtual void ProviderItem_TextChanged(object sender, EventArgs e)
        {
            string str;
            ToolStripItem providerToolStripItem = sender as ToolStripItem;
            if ((providerToolStripItem != null) && !SharedUtil.IsEmpty(str = this[providerToolStripItem]))
            {
                this.RefreshConsumers(providerToolStripItem, str);
            }
            if (e != null)
            {
                foreach (CommandManager manager in globalInstances)
                {
                    if ((manager != this) && manager.isGlobal)
                    {
                        manager.ProviderItem_TextChanged(sender, null);
                    }
                }
            }
        }

        internal void RefreshConsumers()
        {
            this.RefreshConsumers(null, null);
        }

        internal void RefreshConsumers(ToolStripItem providerToolStripItem, string providerCommandKey)
        {
            foreach (KeyValuePair<ToolStripItem, string> pair in this.consumers)
            {
                ToolStripItem item;
                if (((!SharedUtil.IsEmpty(providerCommandKey) && (pair.Value == providerCommandKey)) && ((item = providerToolStripItem) != null)) || ((item = this[pair.Value]) != null))
                {
                    ToolStripMenuItem item2;
                    ToolStripMenuItem item3;
                    pair.Key.Enabled = item.Enabled;
                    pair.Key.Image = item.Image;
                    pair.Key.Text = item.Text;
                    if (((item2 = pair.Key as ToolStripMenuItem) != null) && ((item3 = item as ToolStripMenuItem) != null))
                    {
                        item2.Checked = item3.Checked;
                        item2.ShortcutKeyDisplayString = item3.ShortcutKeyDisplayString;
                        item2.ShortcutKeys = item3.ShortcutKeys;
                        item2.ShowShortcutKeys = item3.ShowShortcutKeys;
                    }
                    else
                    {
                        ToolStripButton button2;
                        if (((item2 = pair.Key as ToolStripMenuItem) != null) && ((button2 = item as ToolStripButton) != null))
                        {
                            item2.Checked = button2.Checked;
                        }
                        else
                        {
                            ToolStripButton button;
                            if (((button = pair.Key as ToolStripButton) != null) && ((button2 = item as ToolStripButton) != null))
                            {
                                button.Checked = button2.Checked;
                            }
                            else if (((button = pair.Key as ToolStripButton) != null) && ((item3 = item as ToolStripMenuItem) != null))
                            {
                                button.Checked = item3.Checked;
                            }
                        }
                    }
                }
            }
        }

        [DefaultValue(""), Localizable(false), ExtenderProvidedProperty]
        internal void SetCommandKeyConsumed(ToolStripItem commandItem, string key)
        {
            if (SharedUtil.IsEmpty(key))
            {
                this.consumers.Remove(commandItem);
                commandItem.Click -= this.consumerItem_Click;
            }
            else
            {
                this.consumers[commandItem] = key;
                commandItem.Click += this.consumerItem_Click;
            }
            this.RefreshConsumers();
        }

        [ExtenderProvidedProperty, DefaultValue(""), Localizable(false)]
        internal void SetCommandKeyProvided(ToolStripItem commandItem, string key)
        {
            this[commandItem] = key;
            this.RefreshConsumers();
        }

        bool IExtenderProvider.CanExtend(object extendee)
        {
            return (extendee is ToolStripItem);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Dictionary Commands
        {
            get
            {
                if (!this.isGlobal)
                {
                    return this.localCommands;
                }
                return globalCommands;
            }
        }

        internal static Keys[] CommandShortcutKeys
        {
            get
            {
                List<Keys> list = new List<Keys>();
                foreach (ToolStripItem item in globalCommands.Values)
                {
                    ToolStripMenuItem item2;
                    if ((((item2 = item as ToolStripMenuItem) != null) && (item2.ShortcutKeys != Keys.None)) && !list.Contains(item2.ShortcutKeys))
                    {
                        list.Add(item2.ShortcutKeys);
                    }
                }
                return list.ToArray();
            }
        }

        internal static Shortcut[] CommandShortcuts
        {
            get
            {
                return SharedUtil.CreateArray<Shortcut, Keys>(CommandShortcutKeys, value => (Shortcut) value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Dictionary GlobalCommands
        {
            get
            {
                return globalCommands;
            }
        }

        [Localizable(false), DefaultValue(false)]
        internal bool IsGlobal
        {
            get
            {
                return this.isGlobal;
            }
            set
            {
                if ((this.isGlobal != value) && (this.isGlobal = value))
                {
                    foreach (KeyValuePair<string, ToolStripItem> pair in this.localCommands)
                    {
                        globalCommands[pair.Key] = pair.Value;
                    }
                    this.localCommands.Clear();
                }
                this.RefreshConsumers();
            }
        }

        internal string this[ToolStripItem commandItem]
        {
            get
            {
                foreach (KeyValuePair<string, ToolStripItem> pair in this.Commands)
                {
                    if (pair.Value == commandItem)
                    {
                        return pair.Key;
                    }
                }
                return string.Empty;
            }
            set
            {
                string str = this[commandItem];
                if (SharedUtil.IsEmpty(value) && !SharedUtil.IsEmpty(str))
                {
                    if (commandItem is ToolStripMenuItem)
                    {
                        ((ToolStripMenuItem) commandItem).CheckedChanged -= this.providerItem_CheckedChanged;
                    }
                    else if (commandItem is ToolStripButton)
                    {
                        ((ToolStripButton) commandItem).CheckedChanged -= this.providerItem_CheckedChanged;
                    }
                    commandItem.EnabledChanged -= this.providerItem_EnabledChanged;
                    commandItem.TextChanged -= this.providerItem_TextChanged;
                    this.Commands.Remove(str);
                }
                else if (!SharedUtil.IsEmpty(value))
                {
                    if (!SharedUtil.IsEmpty(str))
                    {
                        if (commandItem is ToolStripMenuItem)
                        {
                            ((ToolStripMenuItem) commandItem).CheckedChanged -= this.providerItem_CheckedChanged;
                        }
                        else if (commandItem is ToolStripButton)
                        {
                            ((ToolStripButton) commandItem).CheckedChanged -= this.providerItem_CheckedChanged;
                        }
                        commandItem.EnabledChanged -= this.providerItem_EnabledChanged;
                        commandItem.TextChanged -= this.providerItem_TextChanged;
                        this.Commands.Remove(str);
                    }
                    else
                    {
                        if (commandItem is ToolStripMenuItem)
                        {
                            ((ToolStripMenuItem) commandItem).CheckedChanged += this.providerItem_CheckedChanged;
                        }
                        else if (commandItem is ToolStripButton)
                        {
                            ((ToolStripButton) commandItem).CheckedChanged += this.providerItem_CheckedChanged;
                        }
                        commandItem.EnabledChanged += this.providerItem_EnabledChanged;
                        commandItem.TextChanged += this.providerItem_TextChanged;
                    }
                    this.Commands[value] = commandItem;
                }
            }
        }

        internal ToolStripItem this[string key]
        {
            get
            {
                ToolStripItem item;
                if (!this.Commands.TryGetValue(key, out item))
                {
                    return null;
                }
                return item;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Dictionary LocalCommands
        {
            get
            {
                return this.localCommands;
            }
        }

        internal class Dictionary : Dictionary<string, ToolStripItem>
        {
        }
    }
}

