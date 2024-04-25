import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Mascota } from 'src/app/interface/mascota';
import { MascotaService } from 'src/app/services/mascota.service';

@Component({
  selector: 'app-ver-mascota',
  templateUrl: './ver-mascota.component.html',
  styleUrls: ['./ver-mascota.component.css']
})
export class VerMascotaComponent implements OnInit {

  id!: number;
  mascota!: Mascota; 
  loading: boolean = false;

  mascota$!: Observable<Mascota> //Que la variable termine con $ significa que es un observable, es un convenio // PIPE ASYNC

  constructor(private _mascotaService: MascotaService,
    private aRoute: ActivatedRoute
  ) 
  {
     this.id = +this.aRoute.snapshot.paramMap.get('id')!; // Con el + se transforma a int, la ! es para decir que puede ser nulo.
  }

  ngOnInit(): void {
    // this.aRoute.params.subscribe(data => { //Para hacer esto hay que crear una subscripcion y destruirla despues (VER TUTORIAL)
    //   this.id = data['id'];
    // })
    this.obtenerMascota() 
    //this.mascota$ = this._mascotaService.getMascota(this.id) // PIPE ASYNC
  }

  obtenerMascota()
  {
    this.loading = true;
    this._mascotaService.getMascota(this.id).subscribe(data =>
      {
        this.loading = false;
        this.mascota = data;
      }
    )
  }
}
