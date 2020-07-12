import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup,  Validators, FormBuilder } from '@angular/forms';
import { BsLocaleService} from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { deLocale } from 'ngx-bootstrap/locale';
import { ToastrService, Toast } from 'ngx-toastr';

defineLocale('pt-br', deLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  titulo = 'Eventos';
  eventoFiltrado: Evento[];
  eventos: Evento[];
  evento: Evento;
  modoSalvar = 'post';
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  bodyDeletarEvento = '';
  registerForm: FormGroup;
  file: File;
  dataAtual: string;

  _filtroLista = '';
  fileNameToUpdate: string;

  constructor(
        private eventoService: EventoService
      , private modalService: BsModalService
      , private fb: FormBuilder
      , private localeService: BsLocaleService
      , private toastr: ToastrService
      )
      {
        this.localeService.use('pt-br');
       }


  get filtroLista(): string{
    return this._filtroLista;
  }
  set filtroLista(value: string){
      this._filtroLista = value;
      this.eventoFiltrado = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  filtrarEventos(filtrarPor: string): Evento[]{
    filtrarPor = filtrarPor.toLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  editarEvento(evento: Evento, template: any){
     this.modoSalvar = 'put';
     this.openModal(template);
     this.evento = Object.assign({}, evento);
     this.fileNameToUpdate = evento.imagemUrl.toString();
     this.evento.imagemUrl = '';
     this.registerForm.patchValue(this.evento);
  }

  novoEvento(template: any){
     this.modoSalvar = 'post';
     this.openModal(template);
  }

  excluirEvento(evento: Evento, template: any) {
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, CÃ³digo: ${evento.tema}`;
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
          template.hide();
          this.getEventos();
          this.toastr.success('Deletado com Sucesso');
        }, error => {
          this.toastr.error('Erro ao tentar Deletar');
          console.log(error);
        }
    );
  }

  openModal(template: any)
  {
    this.registerForm.reset();
    template.show();
  }

  alternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  onFileChange(event)
  {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length)
    {
      this.file = event.target.files;
      console.log(this.file);
    }
  }

  uploadImagem()
  {
    if (this.modoSalvar = 'post'){
      const nomeArquivo = this.evento.imagemUrl.split( '\\' , 3 );
      this.evento.imagemUrl = nomeArquivo[2];
      this.eventoService.postUpload(this.file, nomeArquivo[2])
      .subscribe(
        () => {
            this.dataAtual = new Date().getMilliseconds().toString();
            this.getEventos();
        }
      );
    }else {
      this.evento.imagemUrl = this.fileNameToUpdate;
      this.eventoService.postUpload(this.file, this.fileNameToUpdate)
      .subscribe(
        () => {
            this.dataAtual = new Date().getMilliseconds().toString();
            this.getEventos();
        }
      );
    }
  }

  salvarAlteracao(templates: any){
    if (this.registerForm.valid){
      if (this.modoSalvar === 'post'){
          this.evento = Object.assign({}, this.registerForm.value);
          this.uploadImagem();
          this.eventoService.postEvento(this.evento).subscribe(
            (novoEvento: Evento) => {
              console.log(novoEvento);
              templates.hide();
              this.getEventos();
              this.toastr.success('Inserido com Sucesso');
            },
            error => {
          this.toastr.error(`Erro ao Inserir: ${error}`);
          console.log(error);
            }
          );
      }else{
        this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
        this.uploadImagem();
        this.eventoService.putEvento(this.evento).subscribe(
            (novoEvento: Evento) => {
              console.log(novoEvento);
              templates.hide();
              this.getEventos();
              this.toastr.success('Editado com Sucesso');
            },
            error => {
              this.toastr.error(`Erro ao Editar: ${error}`);
              console.log(error);
            }
          );
      }
    }
  }

  validation(){
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      imagemUrl: ['', Validators.required],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(12000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    })
  }

  getEventos(){
    this.eventoService.getAllEvento().subscribe(
      (_eventos: Evento[]) => {
      this.eventos = _eventos;
      this.eventoFiltrado = this.eventos;
    },
      error => {
        this.toastr.error(`Erro ao tentar Carregar eventos: ${error}`);
      }
    );
  }
}
