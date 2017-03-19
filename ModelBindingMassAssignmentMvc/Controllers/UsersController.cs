using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelBindingMassAssignmentMvc.Models;
using ModelBindingMassAssignmentMvc.ViewModels;

namespace ModelBindingMassAssignmentMvc.Controllers
{
	public class UsersController : Controller
	{
		private ModelBindingMassAssignmentMvcContext db = new ModelBindingMassAssignmentMvcContext();

		#region Index, Create-GET
		// GET: Users
		public ActionResult Index()
		{
			return View(db.Users.ToList());
		}

		// GET: Users/Create
		public ActionResult Create()
		{
			return View();
		}
		#endregion

		// POST: Users/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(User user)
		{
			if (ModelState.IsValid)
			{
				db.Users.Add(user);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(user);
		}

		// GET: Users/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User user = db.Users.Find(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: Users/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Exclude = "IsAdmin")] User user)
		{
			if (ModelState.IsValid)
			{
				db.Entry(user).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(user);
		}

		public ActionResult Edit2(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User user = db.Users.Find(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: Users/Edit2/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit2([Bind(Include = "Id,Username")] User user)
		{
			if (ModelState.IsValid)
			{
				db.Entry(user).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(user);
		}

		public ActionResult Edit3(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User user = db.Users.Find(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(new UserEditViewModel() { Id = user.Id, Username = user.Username });
		}

		// POST: Users/Edit3/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit3(UserEditViewModel userViewModel)
		{
			if (ModelState.IsValid)
			{
				User user = this.db.Users.Single(u => u.Id == userViewModel.Id);
				user.Username = userViewModel.Username;
				db.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(userViewModel);
		}

		public ActionResult Edit4(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User user = db.Users.Find(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit4(int id)
		{
			var user = this.db.Users.Single(u => u.Id == id);
			if (user == null)
			{
				return HttpNotFound();
			}

			TryUpdateModel(user);  // , includeProperties: new[] { nameof(user.Id), nameof(user.Username) });

			if (ModelState.IsValid)
			{
				db.Entry(user).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(user);
		}

		// GET: Users/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User user = db.Users.Find(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: Users/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			User user = db.Users.Find(id);
			db.Users.Remove(user);
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
	}
}
