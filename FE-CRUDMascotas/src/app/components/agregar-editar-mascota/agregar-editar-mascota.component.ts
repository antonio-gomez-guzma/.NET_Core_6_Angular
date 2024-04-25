import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Mascota } from 'src/app/interface/mascota';
import { MascotaService } from 'src/app/services/mascota.service';

@Component({
  selector: 'app-agregar-editar-mascota',
  templateUrl: './agregar-editar-mascota.component.html',
  styleUrls: ['./agregar-editar-mascota.component.css']
})
export class AgregarEditarMascotaComponent implements OnInit {
  loading: boolean;
  form: FormGroup;

  constructor(private fb:FormBuilder, 
    private _mascotaService: MascotaService,
    private _snackBar: MatSnackBar
  ) {
    this.loading = false;
    this.form = this.fb.group(
      {
        nombre: ['',Validators.required],
        raza: ['',Validators.required],
        color: ['',Validators.required],
        edad: ['',Validators.required],
        peso: ['',Validators.required]
      }
    )
   }


  ngOnInit(): void {
  }

  agregarMascota()
  {
    //const nombre = this.form.get('nombre')?.value;

    const nombre = this.form.value.nombre;

    const mascota: Mascota = {
      nombre: this.form.value.nombre,
      raza: this.form.value.raza,
      color: this.form.value.color,
      edad: this.form.value.edad,
      peso: this.form.value.peso

    }

    //Enviamos objeto al back-end
    this._mascotaService.addMascota(mascota).subscribe(data => 
      {
        this.mensajeExito();
      }
    )
  }

  mensajeExito()
  {
    this._snackBar.open("La Mascota fue registrada con exito", '', {
      duration: 2000,
      horizontalPosition: 'right'
    });
  }

}
