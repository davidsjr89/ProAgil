import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-titiulo',
  templateUrl: './titiulo.component.html',
  styleUrls: ['./titiulo.component.css']
})
export class TitiuloComponent implements OnInit {
  @Input() titulo: string;
  constructor() { }

  ngOnInit() {
  }

}
