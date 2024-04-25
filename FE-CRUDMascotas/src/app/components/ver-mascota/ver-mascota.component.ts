import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Mascota } from 'src/app/interface/mascota';
import { MascotaService } from 'src/app/services/mascota.service';

@Component({
  selector: 'app-ver-mascota',
  templateUrl: './ver-mascota.component.html',
  styleUrls: ['./ver-mascota.component.css']
})
export class VerMascotaComponent implements OnInit {

  id: number;
  mascota!: Mascota; 

  constructor(private _mascotaService: MascotaService,
    private aRoute: ActivatedRoute
  ) 
  {
    this.id = +this.aRoute.snapshot.paramMap.get('id')!; // Con el + se transforma a int, la ! es para decir que puede ser nulo.
  }

  ngOnInit(): void {
    this.obtenerMascota()
  }

  obtenerMascota()
  {
    this._mascotaService.getMascota(this.id).subscribe(data =>
      {
        this.mascota = data;
      }
    )
  }
}
