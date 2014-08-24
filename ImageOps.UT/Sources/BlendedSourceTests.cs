using System.Drawing;
using System.Linq;
using ImageOps.Blenders;
using ImageOps.Sources;
using NUnit.Framework;

namespace ImageOps.UT.Sources
{
    [TestFixture]
    public class BlendedSourceTests
    {
        [Test]
        public void ShouldBlendImagesUsingNormalBlending()
        {
            var back = BitmapCreator.Create(3, 1, Color.White);
            var front = BitmapCreator.Create(new[,]
                {
                    {Color.Black, Color.Transparent, Color.FromArgb(127, Color.Black)}
                });

            var result = BlendToArray(back, front, new NormalBlend());
            Assert.That(result, Is.EqualTo(new[]
                {
                    PixelColor.FromRgb(0, 0, 0),
                    PixelColor.FromRgb(255, 255, 255),
                    PixelColor.FromRgb(127, 127, 127)
                }));
        }

        [Test]
        public void ShouldBlendImagesUsingMultiplyBlending()
        {
            var back = BitmapCreator.Create(3, 1, Color.White);
            var front = BitmapCreator.Create(new[,]
                {
                    {Color.Black, Color.White, Color.FromArgb(10, 80, 70)}
                });

            var result = BlendToArray(back, front, new MultiplyBlend());
            Assert.That(result, Is.EqualTo(new[]
                {
                    PixelColor.FromRgb(0, 0, 0),
                    PixelColor.FromRgb(255, 255, 255),
                    PixelColor.FromRgb(10, 80, 70)
                }));
        }

        [Test]
        public void ShouldBlendImagesUsingGrainMergeBlending()
        {
            var back = BitmapCreator.Create(3, 1, Color.White);
            var front = BitmapCreator.Create(new[,]
                {
                    {Color.Black, Color.White, Color.FromArgb(10, 80, 70)}
                });

            var result = BlendToArray(back, front, new GrainMergeBlend());
            Assert.That(result, Is.EqualTo(new[]
                {
                    PixelColor.FromRgb(127, 127, 127),
                    PixelColor.FromRgb(255, 255, 255),
                    PixelColor.FromRgb(137, 207, 197)
                }));
        }

        [Test]
        public void ShouldBlendImagesUsingBurnBlending()
        {
            var back = BitmapCreator.Create(new[,]
                {
                    {Color.Red, Color.Green, Color.Blue, Color.Gray}
                });
            var front = BitmapCreator.Create(new[,]
                {
                    {Color.Green, Color.Yellow, Color.Gray, Color.Magenta}
                });

            var result = BlendToArray(back, front, new BurnBlend());
            Assert.That(result, Is.EqualTo(new[]
                {
                    Color.Black,
                    PixelColor.FromRgb(0, 128, 0),
                    PixelColor.FromRgb(0, 0, 255),
                    PixelColor.FromRgb(128, 0, 128)
                }));
        }

        [Test]
        public void ShouldBlendTransparentImagesUsingMultiplyBlending()
        {
            var back = BitmapCreator.Create(new[,]
                {
                    {Color.Transparent, Color.White, Color.FromArgb(127, Color.White), Color.FromArgb(127, Color.White)}
                });

            var front = BitmapCreator.Create(new[,]
                {
                    {Color.Red, Color.Red, Color.Red, Color.FromArgb(200, Color.Red)}
                });

            var result = BlendToArray(back, front, new MultiplyBlend());

            Assert.That(result, Is.EqualTo(new[]
                {
                    PixelColor.FromArgb(0, 0, 0, 0),
                    PixelColor.FromArgb(255, 255, 0, 0),
                    PixelColor.FromArgb(127, 255, 85, 85),
                    PixelColor.FromArgb(127, 255, 85, 85)
                }));
        }

        [Test]
        public void ShouldBlendImagesUsingAddBlending()
        {
            var back = BitmapCreator.Create(new[,]
                {
                    {
                        Color.Transparent, Color.White, Color.Black, Color.Black, Color.Black,
                        Color.FromArgb(127, 120, 130, 140), Color.FromArgb(127, 120, 130, 140)
                    }
                });

            var front = BitmapCreator.Create(new[,]
                {
                    {
                        Color.White, Color.White, Color.Black, Color.FromArgb(127, 127, 127), Color.White,
                        Color.FromArgb(10, 20, 30), Color.FromArgb(100, 10, 20, 30)
                    }
                });

            var result = BlendToArray(back, front, new AddBlend());

            Assert.That(result, Is.EqualTo(new[]
                {
                    PixelColor.FromArgb(0, 0, 0, 0),
                    Color.White,
                    Color.Black,
                    PixelColor.FromRgb(127, 127, 127),
                    Color.White,
                    PixelColor.FromArgb(127, 126, 143, 159),
                    PixelColor.FromArgb(127, 125, 141, 156)
                }));
        }

        [Test]
        public void ShouldApplyAlphaMask()
        {
            var bitmap = BitmapCreator.Create(new[,]
                {
                    {
                        Color.Transparent, Color.White, Color.White, Color.FromArgb(127, Color.White),
                        Color.FromArgb(127, Color.White)
                    }
                });

            var mask = BitmapCreator.Create(new[,]
                {
                    {
                        Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 0, 0), Color.FromArgb(0, 0, 0, 0),
                        Color.FromArgb(255, 0, 0, 0), Color.FromArgb(127, 0, 0, 0)
                    }
                });

            var result = BlendToArray(bitmap, mask, new AlphaMaskBlend(ColorChannel.Alpha));

            Assert.That(result, Is.EqualTo(new[]
                {
                    PixelColor.FromArgb(0, 255, 255, 255),
                    PixelColor.FromArgb(255, 255, 255, 255),
                    PixelColor.FromArgb(0, 255, 255, 255),
                    PixelColor.FromArgb(127, 255, 255, 255),
                    PixelColor.FromArgb(63, 255, 255, 255)
                }));
        }

        [Test]
        [TestCase(ColorChannel.Alpha, 50)]
        [TestCase(ColorChannel.Red, 100)]
        [TestCase(ColorChannel.Green, 150)]
        [TestCase(ColorChannel.Blue, 200)]
        public void ShoukdApplyAlphaMaskUsingProperChannel(ColorChannel channel, byte expectedAlpha)
        {
            var color = PixelColor.FromRgb(100, 100, 100).AsPixelSource(1, 1);
            var mask = PixelColor.FromArgb(50, 100, 150, 200).AsPixelSource(1, 1);
            var result = BlendToArray(color, mask, new AlphaMaskBlend(channel));
            Assert.That(result.Single().A, Is.EqualTo(expectedAlpha));
        }

        private static PixelColor[] BlendToArray(Bitmap back, Bitmap front, IBlendingMethod blendingMethod)
        {
            return BlendToArray(back.AsPixelSource(), front.AsPixelSource(), blendingMethod);
        }

        private static PixelColor[] BlendToArray(IPixelSource back, IPixelSource front, IBlendingMethod blendingMethod)
        {
            return new BlendedSource(blendingMethod, back, front).OpenStream().ToArray();
        }
    }
}