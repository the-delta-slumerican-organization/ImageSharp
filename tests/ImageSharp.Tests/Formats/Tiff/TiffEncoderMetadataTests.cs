// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using SixLabors.ImageSharp.PixelFormats;

namespace SixLabors.ImageSharp.Tests
{
    using Xunit;

    using ImageSharp.Formats;
    using ImageSharp.Formats.Tiff;
    using System.Collections.Generic;

    using SixLabors.ImageSharp.Metadata;
    using SixLabors.ImageSharp.Metadata.Profiles.Exif;

    public class TiffEncoderMetadataTests
    {
        public static object[][] BaselineMetadataValues = new[] { new object[] { TiffTags.Artist, TiffMetadataNames.Artist, "My Artist Name" },
                                                                  new object[] { TiffTags.Copyright, TiffMetadataNames.Copyright, "My Copyright Statement" },
                                                                  new object[] { TiffTags.DateTime, TiffMetadataNames.DateTime, "My DateTime Value" },
                                                                  new object[] { TiffTags.HostComputer, TiffMetadataNames.HostComputer, "My Host Computer Name" },
                                                                  new object[] { TiffTags.ImageDescription, TiffMetadataNames.ImageDescription, "My Image Description" },
                                                                  new object[] { TiffTags.Make, TiffMetadataNames.Make, "My Camera Make" },
                                                                  new object[] { TiffTags.Model, TiffMetadataNames.Model, "My Camera Model" },
                                                                  new object[] { TiffTags.Software, TiffMetadataNames.Software, "My Imaging Software" }};

        [Fact]
        public void AddMetadata_SetsImageResolution()
        {
            Image<Rgba32> image = new Image<Rgba32>(100, 100);
            image.Metadata.HorizontalResolution = 40.0;
            image.Metadata.VerticalResolution = 50.5;
            TiffEncoderCore encoder = new TiffEncoderCore(null);

            List<TiffIfdEntry> ifdEntries = new List<TiffIfdEntry>();
            encoder.AddMetadata(image, ifdEntries);

            Assert.Equal(new Rational(40, 1), ifdEntries.GetUnsignedRational(TiffTags.XResolution));
            Assert.Equal(new Rational(101, 2), ifdEntries.GetUnsignedRational(TiffTags.YResolution));
            Assert.Equal(TiffResolutionUnit.Inch, (TiffResolutionUnit?)ifdEntries.GetInteger(TiffTags.ResolutionUnit));
        }

        /*
        [Theory]
        [MemberData(nameof(BaselineMetadataValues))]
        public void AddMetadata_SetsAsciiMetadata(ushort tag, string metadataName, string metadataValue)
        {
            Image<Rgba32> image = new Image<Rgba32>(100, 100);
            image.Metadata.Properties.Add(new ImageProperty(metadataName, metadataValue));
            TiffEncoderCore encoder = new TiffEncoderCore(null);

            List<TiffIfdEntry> ifdEntries = new List<TiffIfdEntry>();
            encoder.AddMetadata(image, ifdEntries);

            Assert.Equal(metadataValue + "\0", ifdEntries.GetAscii(tag));
        } */
    }
}
