using Discord.WebSocket;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace OutBot.Classes.Services
{
    public class ImageService
    {
        private readonly HttpClient httpClient;

        public ImageService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Stream> GenerateWelcomeImage(SocketGuildUser guildUser) { 

            using Image image = Image.Load("Resources/WelcomeBannerBase.png");

            int width = image.Width;
            int height = image.Height;
            const int profilePictureSize = 128;

            var profilePictureUrl = guildUser.GetAvatarUrl(size: 128) ?? guildUser.GetDefaultAvatarUrl();
            var profilePictureStream = await httpClient.GetStreamAsync(profilePictureUrl);
            var profilePicture = await Image.LoadAsync<Rgba32>(profilePictureStream);
            profilePicture.Mutate(ctx => ctx.Resize(profilePictureSize, profilePictureSize));

            var profilePictureLocation = new Point(131, height / 2 - profilePictureSize / 2);
            profilePicture = profilePicture.Clone(x => ConvertToAvatar(x, new Size(profilePictureSize, profilePictureSize), profilePictureSize / 2));
            image.Mutate(ctx => ctx.DrawImage(profilePicture, profilePictureLocation, 1f));

            var welcomeFont = SystemFonts.CreateFont("Rubik", 20);
            var welcomeText = $"WILLKOMMEN AUF {guildUser.Guild.Name}!";
            var welcomeTextColor = new Color(new Rgba32(0xe8, 0xe8, 0xe8));
            var welcomeTextArea = new RectangleF(260, 75, 410, 30);
            this.createCenteredTextBox(image, welcomeText, welcomeFont, welcomeTextColor, welcomeTextArea);

            var userNameFont = SystemFonts.CreateFont("Rubik", 44);
            var userNameText = $"@{guildUser.Username.ToUpper()}";
            var userNameTextColor = new Color(new Rgba32(0x00, 0xF4, 0x8E));
            var userNameTextArea = new RectangleF(260, 80, 410, 71);
            this.createCenteredTextBox(image, userNameText, userNameFont, userNameTextColor, userNameTextArea);

            var stream = new MemoryStream();
            await image.SaveAsPngAsync(stream);
            stream.Position = 0;

            return stream;
        }

        private void createCenteredTextBox(Image image, string text, Font font, Color fontColor, RectangleF textArea)
        {
            FontRectangle textSize;
            do
            {
                textSize = TextMeasurer.MeasureSize(text, new TextOptions(font));
                font = SystemFonts.CreateFont(font.Family.Name, font.Size - 1, FontStyle.Bold);
                if (font.Size == 1) break;
            } while (textSize.Width > textArea.Width && font.Size > 1);


            // Text positionieren
            var textLocation = new PointF(
                textArea.X + (textArea.Width - textSize.Width) / 2,
                textArea.Y + (textArea.Height - textSize.Height) / 2
            );
            image.Mutate(ctx => ctx.DrawText(text, font, fontColor, textLocation));
        }

        private IPath CreateCircle(float centerX, float centerY, float radius)
        {
            return new EllipsePolygon(centerX, centerY, radius);
        }

        /// <src>https://github.com/SixLabors/Samples/blob/24326f576f712b190b8f05ab230a5c9b95d90fcf/ImageSharp/AvatarWithRoundedCorner/Program.cs</src>
        private static IImageProcessingContext ConvertToAvatar(IImageProcessingContext context, Size size, float cornerRadius)
        {
            return ApplyRoundedCorners(context.Resize(new ResizeOptions
            {
                Size = size,
                Mode = ResizeMode.Crop
            }),
            cornerRadius);

        }

        /// <src>https://github.com/SixLabors/Samples/blob/24326f576f712b190b8f05ab230a5c9b95d90fcf/ImageSharp/AvatarWithRoundedCorner/Program.cs</src>
        private static IImageProcessingContext ApplyRoundedCorners(IImageProcessingContext context, float cornerRadius)
        {
            Size size = context.GetCurrentSize();
            IPathCollection corners = BuildCorners(size.Width, size.Height, cornerRadius);

            context.SetGraphicsOptions(new GraphicsOptions()
            {
                Antialias = true,

                AlphaCompositionMode = PixelAlphaCompositionMode.DestOut
            });

            foreach (IPath path in corners)
            {
                context = context.Fill(Color.Red, path);
            }

            return context;
        }

        /// <src>https://github.com/SixLabors/Samples/blob/24326f576f712b190b8f05ab230a5c9b95d90fcf/ImageSharp/AvatarWithRoundedCorner/Program.cs</src>
        private static PathCollection BuildCorners(int imageWidth, int imageHeight, float cornerRadius)
        {
            var rect = new RectangularPolygon(-0.5f, -0.5f, cornerRadius, cornerRadius);

            IPath cornerTopLeft = rect.Clip(new EllipsePolygon(cornerRadius - 0.5f, cornerRadius - 0.5f, cornerRadius));

            float rightPos = imageWidth - cornerTopLeft.Bounds.Width + 1;
            float bottomPos = imageHeight - cornerTopLeft.Bounds.Height + 1;

            IPath cornerTopRight = cornerTopLeft.RotateDegree(90).Translate(rightPos, 0);
            IPath cornerBottomLeft = cornerTopLeft.RotateDegree(-90).Translate(0, bottomPos);
            IPath cornerBottomRight = cornerTopLeft.RotateDegree(180).Translate(rightPos, bottomPos);

            return new PathCollection(cornerTopLeft, cornerBottomLeft, cornerTopRight, cornerBottomRight);
        }
    }
}
