import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-agregar-editar-mascota',
  templateUrl: './agregar-editar-mascota.component.html',
  styleUrls: ['./agregar-editar-mascota.component.css']
})
export class AgregarEditarMascotaComponent implements OnInit {
  loading: boolean;

  constructor() {
    this.loading = false;
   }


  ngOnInit(): void {
  }

}
