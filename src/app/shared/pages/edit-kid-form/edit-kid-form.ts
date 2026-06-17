import { AsyncPipe, Location } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, shareReplay, tap } from 'rxjs';
import { AvatarOption, AVATARS } from '../../../consts/avatars';
import { KidsService } from '../../../core/services/kids/kids.service';
import { Kid } from '../../../core/types/dtos/kid';
import { Header } from '../../components/header/header';
import { LoadingEmoji } from '../../components/loading-emoji/loading-emoji';
import { LoadingScreen } from '../loading-screen/loading-screen';

@Component({
  selector: 'app-edit-kid-form',
  imports: [AsyncPipe, LoadingScreen, ReactiveFormsModule, LoadingEmoji, Header],
  templateUrl: './edit-kid-form.html',
  styleUrl: './edit-kid-form.scss',
})
export class EditKidForm implements OnInit {
  private kidsService = inject(KidsService);
  private location = inject(Location);
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  saveLoading = signal(false);
  deleteLoading = signal(false);

  toastVisible = false;
  avatarPickerOpen = false;

  avatarOptions = AVATARS;
  avatarBackground = 'linear-gradient(135deg, #87ceeb 0%, #5aafd4 100%)';

  deleteButtonText = signal('Remove Kid');
  deleteButtonClicked = false;

  kidId!: number;

  kid$!: Observable<Kid>;

  form = this.fb.nonNullable.group({
    name: ['', { validators: [Validators.required] }],
    age: [0, { validators: [Validators.required] }],
    avatar: ['🧒', { validators: [Validators.required] }],
    dayStreak: [0, { validators: [Validators.required] }],
    spendablePoints: [0, { validators: [Validators.required] }],
  });

  ngOnInit() {
    this.kidId = Number(this.route.snapshot.paramMap.get('id'));

    this.kid$ = this.kidsService.getKidById$(this.kidId).pipe(
      tap((kid) => {
        if (!kid) return;

        this.form.patchValue({
          name: kid.name,
          age: kid.age,
          dayStreak: kid.dayStreak,
          spendablePoints: kid.totalPoints,
          avatar: kid.avatar,
        });
      }),
      shareReplay(1),
    );
  }

  selectAvatar(avatar: AvatarOption) {
    this.avatarBackground = avatar.background;
    this.form.patchValue({ avatar: avatar.emoji });
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.saveLoading.set(true);

    this.kidsService.updateKid$({ id: this.kidId, ...this.form.getRawValue() }).subscribe({
      next: () => {
        console.log('Kid updated successfully');
        this.saveLoading.set(false);
        this.showToast();
      },
      error: () => {
        console.log('Kid updated failed');
        this.saveLoading.set(false);
      },
    });
  }

  delete() {
    if (!this.deleteButtonClicked) {
      this.deleteButtonClicked = true;
      this.deleteButtonText.set('Are you sure?');

      return;
    }

    this.deleteLoading.set(true);

    this.kidsService.deleteKidById$(this.kidId).subscribe({
      next: () => {
        console.log('Kid updated successfully');
        this.deleteLoading.set(false);
        this.router.navigate(['/dashboard/kids']);
      },
      error: () => {
        console.log('Kid updated failed');
        this.deleteLoading.set(false);
      },
    });
  }

  adjustAge(increase = false) {
    const currentAge = this.form.controls.age.value;

    let newAge = increase ? currentAge + 1 : currentAge - 1;

    if (newAge > 99) newAge = 99;
    if (newAge < 1) newAge = 1;

    this.form.controls.age.setValue(newAge);
  }

  showToast() {
    this.toastVisible = true;
    setTimeout(() => {
      this.toastVisible = false;
    }, 2000);
  }

  back() {
    this.location.back();
  }
}
