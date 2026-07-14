import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ChoreService } from '../../../../core/services/chore/chore.service';
import { KidsDataService } from '../../../../core/services/kids/kids-data.service';
import { Chore } from '../../../../core/types/dtos/chore';
import { Kid } from '../../../../core/types/dtos/kid';
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
  private choreService = inject(ChoreService);
  private fb = inject(FormBuilder);
  private kidsDataService = inject(KidsDataService);
  private route = inject(ActivatedRoute);

  loading = signal(false);
  error = signal<string | null>(null);

  choreId!: number;

  vm$!: Observable<{
    kids: Kid[];
    chore: Chore | null;
  }>;

  form = this.fb.group<ChoreFormType>({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    icon: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    assignedKids: new FormControl([], { validators: [Validators.required], nonNullable: true }),
    frequency: new FormControl(ChoreFrequency.Daily, {
      validators: [Validators.required],
      nonNullable: true,
    }),
    difficulty: new FormControl(ChoreDifficulty.Easy, {
      validators: [Validators.required],
      nonNullable: true,
    }),
    dueDay: new FormControl<Date | null>(null),
    points: new FormControl(0, {
      validators: [Validators.required, Validators.min(0)],
      nonNullable: true,
    }),
    description: new FormControl(''),
    isVisible: new FormControl(true, { nonNullable: true }),
  });

  kids$ = this.kidsDataService.kids$;

  ngOnInit() {
    this.choreId = Number(this.route.snapshot.paramMap.get('id'));

    this.loadData();
  }

  loadData() {
    // this.vm$ = forkJoin({
    //   kids: this.kidsDataService.getKids$(),
    //   chore: this.choreService.getById$(this.choreId!),
    // }).pipe(
    //   tap(({ kids, chore }) => {
    //     if (kids.length && !this.form.value.kidId) {
    //       this.form.patchValue({ kidId: kids[0].kidId });
    //     }
    //     if (chore != null) {
    //       this.form.patchValue({
    //         name: chore.name,
    //         icon: chore.icon,
    //         kidId: chore.kidId,
    //         frequency: chore.frequency,
    //         difficulty: chore.difficulty,
    //         dueDay: chore.dueDay,
    //         points: chore.points,
    //         description: chore.description,
    //         isVisible: chore.isVisible,
    //       });
    //     }
    //   }),
    // );
  }

  submit() {
    // if (this.form.invalid) {
    //   this.form.markAllAsTouched();
    //   return;
    // }
    // this.loading.set(true);
    // this.choreService.updateChore$({ ...this.form.getRawValue(), id: this.choreId }).subscribe({
    //   next: () => {
    //     console.log('Chore updated successfully');
    //     this.loading.set(false);
    //     window.history.back();
    //   },
    //   error: (err) => {
    //     console.error('Error updating chore:', err);
    //     this.error.set('Failed to update chore. Please try again.');
    //     this.loading.set(false);
    //     window.history.back();
    //   },
    // });
  }
}
