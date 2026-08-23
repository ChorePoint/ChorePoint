import { AsyncPipe } from '@angular/common';
import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ChoreService } from '../../../../core/services/chore/chore.service';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { ChoreDifficulty } from '../../../../core/types/enums/chore-difficulty';
import { ChoreFrequency } from '../../../../core/types/enums/chore-frequency';
import { ChoreForm } from '../../../../shared/components/chore-form/chore-form';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { ChoreFormGroup as ChoreFormType } from '../../../../shared/types/chore-form-group';

@Component({
  selector: 'app-edit-chore',
  imports: [AsyncPipe, ChoreForm, LoadingScreen],
  templateUrl: './edit-chore.html',
})
export class EditChore implements OnInit {
  private kidService = inject(KidsService);
  private choreService = inject(ChoreService);

  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);

  loading = signal(false);
  error = signal<string | null>(null);

  choreId!: number;

  vm = {
    kids: this.kidService.kids,
  };

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

  constructor() {
    effect(() => {
      const chore = this.choreService.chores().find((c) => c.choreId === this.choreId);

      if (!chore) return;

      this.form.patchValue({
        name: chore.name,
        icon: chore.icon,
        assignedKids: chore.assignedKids,
        frequency: chore.frequency,
        difficulty: chore.difficulty,
        points: chore.points,
        description: chore.description,
      });
    });
  }

  ngOnInit() {
    this.choreId = Number(this.route.snapshot.paramMap.get('id'));
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.loading.set(true);
    this.choreService
      .updateChore$({ ...this.form.getRawValue(), choreId: this.choreId })
      .subscribe({
        next: () => {
          this.loading.set(false);
          window.history.back();
        },
        error: () => {
          this.error.set('Failed to update chore. Please try again.');
          this.loading.set(false);
          window.history.back();
        },
      });
  }
}
