﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using RealEstateApp.Extensions;

namespace RealEstateApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddApplicationServices();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
