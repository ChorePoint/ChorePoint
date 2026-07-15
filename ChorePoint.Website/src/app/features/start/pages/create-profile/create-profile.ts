import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AVATARS } from '../../../../core/consts/avatars';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { LoadingEmoji } from '../../../../shared/components/loading-emoji/loading-emoji';

@Component({
  selector: 'app-create-profile',
  templateUrl: './create-profile.html',
  styleUrl: './create-profile.scss',
  imports: [LoadingEmoji, ReactiveFormsModule],
})
export class CreateProfile {
  private fb = inject(FormBuilder);
  private kidService = inject(KidsService);
  private router = inject(Router);

  loading = signal(false);
  error = signal<string | null>(null);

  avatars = AVATARS;

  form = this.fb.nonNullable.group({
    name: ['', { validators: Validators.required }],
    age: ['', { validators: [Validators.required] }],
    avatar: ['🧒', { validators: [Validators.required] }],
  });

  selectAvatar(avatar: string) {
    this.form.patchValue({ avatar: avatar });
  }

  back() {
    window.history.back();
  }

  submit() {
    console.log(this.form);
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const { name, age, avatar } = this.form.getRawValue();

    this.kidService.createKid$({ name, age, avatar }).subscribe({
      next: () => {
        this.router.navigate(['dashboard/home']);
      },
    });
  }
}
