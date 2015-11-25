using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LicensePlatesDBFirst.Models;
using System.Collections;
using LicensePlatesDBFirst.Models.ViewModels;

namespace LicensePlatesDBFirst.Controllers
{
    public class GamesController : BaseController
    {
        // GET: Games
        public ActionResult Index()
        {
            return RedirectToAction("History");
        }

        // GET: History
        public ActionResult History()
        {
            if (UserId <= 0)
            {
                return View(new List<Game>());
            }

            var games = (UserName.Equals("admin")) ? db.Games.Include(g => g.User) : db.Games.Where(g => g.UserId == UserId).Include(g => g.User);
            return View(games.ToList());
        }

        // GET: Games/NewGame
        public ActionResult NewGame()
        {
            var game = new Game();
            var vm = new NewGameViewModel();

            game.UserId = UserId;
            game.User = db.Users.Where(u => u.Id == UserId).First();
            vm.Game = game;

            vm.Actives = new List<Activeness>();
            var countries = db.Countries.ToList();
            countries.ForEach(c => vm.Actives.Add(new Activeness(c)));

            return View(vm);
        }

        // POST: Games/NewGame
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewGame(NewGameViewModel vm)
        {
            if (ModelState.IsValid)
            {
                G = vm.Game;
                db.Games.Add(G);

                var activeCountries = vm.Actives.Where(a => a.Active).ToList();
                activeCountries.ForEach(a => db.GameCountries.Add(new GameCountry(G.Id, a.CountryId)));

                db.SaveChanges();

                return RedirectToAction("Play");
                //return RedirectToAction("Play", new { id = G.Id }); // don't think we need to use this one
            }

            return View(vm);
        }

        // GET: Games/Play/5
        public ActionResult Play(int? id)
        {
            if (id == null)
            {
                if (G == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                G = db.Games.Find(id);
                if (G == null)
                {
                    return HttpNotFound();
                }
            }

            var vm = new PlayViewModel(G);

            var activeCountries = G.GameCountries.Select(s => s.CountryId).ToList();
            vm.Plates = db.Plates.Where(p => activeCountries.Contains(p.CountryId)).ToList();
            vm.Countries = db.Countries.Where(c => activeCountries.Contains(c.Id)).ToList();
            vm.Regions = db.Regions.Where(r => activeCountries.Contains(r.CountryId)).ToList();

            return View(vm);
        }

        // GET: Games/Settings/5
        public ActionResult Settings(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", game.UserId);
            return View(game);
        }

        // POST: Games/Settings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings([Bind(Include = "Id,Start,Stop,Name,UserId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", game.UserId);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult UpdatePlateSeen(string plateId, string seen)
        {
            int pid = int.Parse(plateId);
            var plateIds = G.GamePlates.Select(p => p.PlateId).ToList();

            if (bool.Parse(seen) && !plateIds.Contains(pid))
            {
                var newPlate = new GamePlate(G.Id, pid);
                G.GamePlates.Add(newPlate);
                db.GamePlates.Add(newPlate);
                db.SaveChanges();
            }
            else if (!bool.Parse(seen) && plateIds.Contains(pid))
            {
                var target = G.GamePlates.Where(p => p.PlateId == pid).FirstOrDefault();
                G.GamePlates.Remove(target);
                target = db.GamePlates.Where(p => p.PlateId == pid && p.GameId == G.Id).FirstOrDefault();
                db.GamePlates.Remove(target);
                db.SaveChanges();
            }

            return Json(new { score = G.Score });
        }
    }
}
