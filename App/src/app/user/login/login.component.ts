import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  titulo = 'Login';
  model: any = {};
  constructor(public router: Router, public authService: AuthService, private toast: ToastrService) { }

  ngOnInit(){
    if (localStorage.getItem('token') !== null){
      this.router.navigate(['/dashboard']);
    }
  }
  login(){
    this.authService.login(this.model)
      .subscribe(
        () => {
          this.router.navigate(['/dashboard']);
          this.toast.success('Logado com Sucesso');
        }, error => {
          this.toast.error('Falha ao tentar Logar');
        }
      );
  }

}
