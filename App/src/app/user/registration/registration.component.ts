import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registerForm: FormGroup;
  constructor(public fb: FormBuilder, private toastr: ToastrService, private authService: AuthService, private router: Router) { }
  user: User;
  ngOnInit() {
    this.validation();
  }
  validation(){
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      passwords : this.fb.group({
        password : ['', [Validators.required, Validators.minLength(4)]],
        confirmPassword : ['', Validators.required]
      }, { validator : this.compararSenhas})
    });
  }
  compararSenhas(fb: FormGroup) {
    const confirmPwdCtrl = fb.get('confirmPassword');
    if (confirmPwdCtrl.errors == null || 'mismatch' in confirmPwdCtrl.errors) {
      if (fb.get('password').value !== confirmPwdCtrl.value) {
        confirmPwdCtrl.setErrors({ mismatch: true});
      } else {
        confirmPwdCtrl.setErrors(null);
      }
    }
  }

  cadastrarUsuario(){
    if (this.registerForm.valid){
      this.user = Object.assign({password: this.registerForm.get('passwords.password').value},
      this.registerForm.value);
      this.authService.register(this.user).subscribe(
        () => {
          this.router.navigate(['/user/login']);
          this.toastr.success('Cadastro Realizado');
        }, error => {
          const err = error.error;
          err.forEach(element => {
            switch (element.code){
              case 'DuplicateUserName':
                this.toastr.error('Cadastro Duplicado!');
                break;
              default:
                this.toastr.error(`Erro no Cadastro! CODE: ${element.code}`);
                break;
            }
          });
        }
      )
    }
  }
}
