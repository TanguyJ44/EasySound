// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasySound.Data;


namespace EasySound.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        private ApplicationDbContext db;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext _db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = _db;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        public IEnumerable<Playlists> ListPL { get; set; }
        public IEnumerable<IEnumerable<Sounds>> ListSounds { get; set; }

        public int CountPL { get; set; }
        public int CountSounds { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        [BindProperty]
        public string Plname { get; set; }

        [BindProperty]
        public string Soundname { get; set; }

        [BindProperty]
        public string Soundlink { get; set; }

        [BindProperty]
        public string Plid { get; set; }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };

        }

        public async Task<IActionResult> OnGetAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Request.Query["action"] == "Delete")
            {
                var deletePlaylist = new Playlists { PlaylistId = Int32.Parse(Request.Query["id"]) };
                db.Playlists.Attach(deletePlaylist);
                db.Playlists.Remove(deletePlaylist);
                db.SaveChanges();
            }

            if (Request.Query["action"] == "DeleteSound")
            {
                var deleteSound = new Sounds { SoundId = Int32.Parse(Request.Query["id"]) };
                db.Sounds.Attach(deleteSound);
                db.Sounds.Remove(deleteSound);
                db.SaveChanges();
            }

            List<Data.Playlists> dataPlaylists = (from Playlists in db.Playlists
                                            where Playlists.UserID == user.Id
                                            select new Data.Playlists
                                            {
                                                PlaylistId = Playlists.PlaylistId,
                                                UserID = Playlists.UserID,
                                                PlaylistName = Playlists.PlaylistName

                                            }).ToList();

            dataPlaylists.Reverse();

            List<List<Data.Sounds>> dataSounds = new List<List<Data.Sounds>>();

            foreach (Playlists playlist in dataPlaylists)
            {
                List<Data.Sounds> plSounds = (from Sounds in db.Sounds
                                                where Sounds.PlaylistId == playlist.PlaylistId.ToString()
                                                select new Data.Sounds
                                                {
                                                    SoundId = Sounds.SoundId,
                                                    PlaylistId = Sounds.PlaylistId,
                                                    SoundName = Sounds.SoundName,
                                                    SoundLink = Sounds.SoundLink

                                                }).ToList();

                if (plSounds != null)
                {
                    dataSounds.Add(plSounds);
                }
            }

            ListPL = dataPlaylists;
            ListSounds = dataSounds;

            CountPL = dataPlaylists.Count;
            CountSounds = dataSounds.Count;

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (Plname != null)
            {
                Playlists playlist = new Playlists();

                playlist.UserID = _userManager.GetUserId(User);
                playlist.PlaylistName = Plname;

                db.Playlists.Add(playlist);
                db.SaveChanges();
            }

            if (Soundname != null && Plid != null)
            {
                Sounds sounds = new Sounds();

                sounds.PlaylistId = Plid;
                sounds.SoundName = Soundname;
                sounds.SoundLink = Soundlink;

                db.Sounds.Add(sounds);
                db.SaveChanges();
            }

            return RedirectToPage();
        }
    }
}
