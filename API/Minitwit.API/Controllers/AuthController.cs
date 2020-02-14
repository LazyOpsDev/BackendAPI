using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minitwit.Models;

namespace Minitwit.API.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpGet("/login")]
        public User LoginGet()
        {
            //if logged in dont do anything
            return null;
        }


        [HttpPost("/login")]
        public User LoginPost()
        {
            //if logged in dont do anything
            return null;
        }

//@app.route('/login', methods=['GET', 'POST'])
//def login():
//    """Logs the user in."""
//    if g.user:
//        return redirect(url_for('timeline'))
//    error = None
//    if request.method == 'POST':
//        user = query_db('''select * from user where
//            username = ? ''', [request.form['username']], one=True)
//        if user is None:
//            error = 'Invalid username'
//        elif not check_password_hash(user['pw_hash'],
//                                     request.form['password']):
//            error = 'Invalid password'
//        else:
//            flash('You were logged in')
//            session['user_id'] = user['user_id']
//            return redirect(url_for('timeline'))
//    return render_template('login.html', error= error)

        //// GET: api/Auth
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Auth/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Auth
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
