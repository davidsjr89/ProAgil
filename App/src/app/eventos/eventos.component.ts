import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  _filtroLista: string;
  get filtroLista(): string{
    return this._filtroLista;
  }
  set filtroLista(value: string){
      this._filtroLista = value;
      this.eventoFiltrado = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  eventoFiltrado: any = [];

  eventos: any = [];
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  filtrarEventos(filtrarPor: string): any{
    filtrarPor = filtrarPor.toLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLowerCase().indexOf(filtrarPor) !== -1,
      evento => evento.local.toLowerCase().indexOf(filtrarPor) !== -1,
      evento => evento.dataEvento.toLowerCase().indexOf(filtrarPor) !== -1,
      evento => evento.qtdPessoas.toLowerCase().indexOf(filtrarPor) !== -1,
      evento => evento.lote.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos(){
    this.http.get('http://localhost:5000/api/values').subscribe( response => {
      console.log(response);
      this.eventos = response;
    },
      error => {console.log(error); }
    );
  }

}
