import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Mascota } from 'src/app/interface/mascota';


const listMascotas: Mascota[] = [
  {nombre: 'Ciro', edad: 3, raza: 'Golden', color: 'Dorado', peso: 13},
  {nombre: 'Kami', edad: 3.5, raza: 'Gata persa', color: 'Negro', peso: 3},
  {nombre: 'Danco', edad: 6, raza: 'Pastor aleman', color: 'Castaño', peso: 20},
  {nombre: 'Emilio', edad: 10, raza: 'Caniche', color: 'Blanco', peso: 2 },
  {nombre: 'Pedro', edad: 5, raza: 'Mapache', color: 'Negro', peso: 10},
  {nombre: 'Poli', edad: 8, raza: 'Camaleón', color: '???', peso: 0.3}
];

@Component({
  selector: 'app-listado-mascota',
  templateUrl: './listado-mascota.component.html',
  styleUrls: ['./listado-mascota.component.css']
})
export class ListadoMascotaComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['name', 'edad', 'raza', 'color', 'peso'];
  dataSource = new MatTableDataSource<Mascota>(listMascotas);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  constructor() { }

  ngOnInit(): void {
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.paginator._intl.itemsPerPageLabel = "Items por página"
  }

}
