import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoadingScreen } from '../../../common/loading-screen/loading-screen';
import { ChoreForm } from '../../../../components/common/chore-form/chore-form';
import { ChoreService } from '../../../../core/services/parent/chore/chore.service';
import { KidsService } from '../../../../core/services/parent/kids/kids.service';
import { DIFFICULTY_OPTIONS } from '../../../../core/consts/common/difficulty-options';
import { CHORE_EMOJIS } from '../../../../core/consts/parent/chore-emojis';
import { DAYS_OF_WEEK } from '../../../../core/consts/common/days-of-week';
import { FREQUENCY_OPTIONS } from '../../../../core/consts/common/frequency-options';
import { ChoreFrequency } from '../../../../core/types/enums/parent/chore-frequency';
import { ChoreDifficulty } from '../../../../core/types/enums/parent/chore-difficulty';
import { ChoreFormGroup } from '../../../../core/types/interfaces/parent/chore-form-group';

@Component({
  selector: 'app-add-chore',
  imports: [ReactiveFormsModule, LoadingScreen, ChoreForm],
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

  kids = this.kidsService.kids;

  form = this.fb.group<ChoreFormGroup>({
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
    console.log(this.form);
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    this.choreService.createChore$(this.form.getRawValue()).subscribe({
      next: () => {
        this.loading.set(false);
        this.form.reset();
      },
      error: () => {
        this.error.set('Failed to create chore. Please try again.');
        this.loading.set(false);
      },
    });

    this.router.navigate(['/dashboard/chores']);
  }
}
