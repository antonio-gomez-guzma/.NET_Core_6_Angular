import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { Mascota } from 'src/app/interface/mascota';
import { MascotaService } from 'src/app/services/mascota.service';



@Component({
  selector: 'app-listado-mascota',
  templateUrl: './listado-mascota.component.html',
  styleUrls: ['./listado-mascota.component.css']
})
export class ListadoMascotaComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['name', 'edad', 'raza', 'color', 'peso', 'acciones'];
  dataSource = new MatTableDataSource<Mascota>();
  loading: boolean = false;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  constructor(private _snackBar: MatSnackBar, 
              private _mascotaService: MascotaService) { }

  ngOnInit(): void {
    this.obtenerMascotas();
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    if(this.dataSource.data.length > 0)
      {
        this.paginator._intl.itemsPerPageLabel = "Items por página"
      }
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  eliminarMascota()
  {
    this.loading = true 
    setTimeout(()=>{

        this._snackBar.open("La Mascota fue eliminada con exito", '', {
          duration: 2000,
          horizontalPosition: 'right'
        });
        this.loading = false;
    },3000);
  }

  obtenerMascotas()
  {
    this.loading = true;
    this._mascotaService.getMascotas().subscribe(data => 
      {
        this.loading = false;
        this.dataSource.data = data;
      }, error => 
        {
          this.loading = false;
          alert("Oppss ocurrio un error");
        }
    )
  }
}
