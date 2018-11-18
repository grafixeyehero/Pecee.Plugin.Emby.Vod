﻿using System;
using System.Collections.Generic;
using System.Reflection;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using Pecee.Emby.Plugin.Vod.Configuration;

namespace Pecee.Emby.Plugin.Vod
{
	public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {

		public static Plugin Instance { get; private set; }

		public override string Name => PluginConfiguration.Name;

		public override string Description => PluginConfiguration.Description;

		public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer)
		{
			Instance = this;
		}

		public IEnumerable<PluginPageInfo> GetPages()
		{
			return new[]
			{
				new PluginPageInfo
				{
					Name = "vod.config",
					EmbeddedResourcePath = GetType().Namespace + ".Configuration.configPage.html"
				},
			};
		}

		public static DynamicImageResponse GetImage(string name)
		{
			var type = typeof(Plugin);
			var path = String.Format("{0}.Images.{1}.png", type.Namespace, name);
			return new DynamicImageResponse
			{
				Format = ImageFormat.Png,
				HasImage = true,
				Stream = type.GetTypeInfo().Assembly.GetManifestResourceStream(path)
			};
		}

    }
}
