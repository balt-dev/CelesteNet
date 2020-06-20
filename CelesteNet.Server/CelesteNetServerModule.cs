﻿using Celeste.Mod.CelesteNet.DataTypes;
using Mono.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Celeste.Mod.CelesteNet.Server {
    public abstract class CelesteNetServerModule : IDisposable {

#pragma warning disable CS8618 // Set on init.
        public CelesteNetServer Server;
        public CelesteNetServerModuleWrapper Wrapper;
#pragma warning restore CS8618

        public virtual void Init(CelesteNetServerModuleWrapper wrapper) {
            Server = wrapper.Server;
            Wrapper = wrapper;

            Server.Data.RegisterHandlersIn(this);

            LoadSettings();
        }

        public virtual void LoadSettings() {
        }

        public virtual void SaveSettings() {
        }

        public virtual void Start() {
        }

        public virtual void Dispose() {
            SaveSettings();

            Server.Data.UnregisterHandlersIn(this);
        }

    }

    public abstract class CelesteNetServerModule<TSettings> : CelesteNetServerModule where TSettings : new() {

        public TSettings Settings = new TSettings();

        public override void LoadSettings() {
            string path = Path.Combine(Path.GetFullPath(Server.Settings.ModuleConfigRoot), $"{Wrapper.ID}.yaml");
            if (!File.Exists(path)) {
                SaveSettings();
                return;
            }

            using (StreamReader reader = new StreamReader(path))
                YamlHelper.DeserializerUsing(Settings ??= new TSettings()).Deserialize<TSettings>(reader);
        }

        public override void SaveSettings() {
            string path = Path.Combine(Path.GetFullPath(Server.Settings.ModuleConfigRoot), $"{Wrapper.ID}.yaml");
            if (File.Exists(path))
                File.Delete(path);

            string? dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (StreamWriter writer = new StreamWriter(path))
                YamlHelper.Serializer.Serialize(writer, Settings ?? new TSettings(), typeof(TSettings));
        }

    }
}
