import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CHORE_EMOJIS } from '../../../../core/consts/chore-emojis';
import { ChoreService } from '../../../../core/services/chore/chore.service';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { ChoreDifficulty } from '../../../../core/types/enums/chore-difficulty';
import { ChoreFrequency } from '../../../../core/types/enums/chore-frequency';
import { ChoreForm } from '../../../../shared/components/chore-form/chore-form';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { ChoreFormGroup as ChoreFormType } from '../../../../shared/types/chore-form-group';
import { DAYS_OF_WEEK } from '../../config/days-of-week';
import { DIFFICULTY_OPTIONS } from '../../config/difficulty-options';
import { FREQUENCY_OPTIONS } from '../../config/frequency-options';

@Component({
  selector: 'app-add-chore',
  imports: [ReactiveFormsModule, AsyncPipe, LoadingScreen, ChoreForm],
  templateUrl: './add-chore.html',
  styleUrl: './add-chore.scss',
})
export class AddChore implements OnInit {
  private choreService = inject(ChoreService);
  private fb = inject(FormBuilder);
  private kidsService = inject(KidsService);
  private router = inject(Router);

  loading = signal(false);
  error = signal<string | null>(null);

  choreDifficultyOptions = DIFFICULTY_OPTIONS;
  choreEmojis = CHORE_EMOJIS;
  daysOfWeek = DAYS_OF_WEEK;
  choreFrequencyOptions = FREQUENCY_OPTIONS;
  choreFrequency = ChoreFrequency;

  kids = this.kidsService.kids$;

  form = this.fb.group<ChoreFormType>({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    icon: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    assignedKids: new FormControl([], {
      validators: [Validators.required],
      nonNullable: true,
    }),
    frequency: new FormControl(ChoreFrequency.Daily, {
      validators: [Validators.required],
      nonNullable: true,
    }),
    difficulty: new FormControl(ChoreDifficulty.Easy, {
      validators: [Validators.required],
      nonNullable: true,
    }),
    points: new FormControl(0, {
      validators: [Validators.required, Validators.min(0)],
      nonNullable: true,
    }),
    description: new FormControl(''),
  });

  ngOnInit() {
    this.loadKids();
  }

  loadKids() {
    if (this.kids().length !== 0 && !this.form.value.assignedKids) {
      this.form.patchValue({
        assignedKids: [
          {
            kidId: this.kids()[0].kidId,
            dayOfWeek: null,
            isVisible: true,
          },
        ],
      });
    }
  }

  selectFrequency(frequency: number) {
    this.form.patchValue({ frequency });
  }

  adjustPoints(amount: number) {
    const current = this.form.get('points')?.value || 0;
    const next = Math.max(50, current + amount);
    this.form.patchValue({ points: next });
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    this.choreService.createChore$(this.form.getRawValue()).subscribe({
      next: () => {
        console.log('Chore created successfully');
        this.loading.set(false);
        this.form.reset();
      },
      error: (err) => {
        console.error('Error creating chore:', err);
        this.error.set('Failed to create chore. Please try again.');
        this.loading.set(false);
      },
    });

    this.router.navigate(['/dashboard/chores']);
  }
}
