import { AsyncPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { map, Observable } from 'rxjs';
import { CHORE_EMOJIS } from '../../../../consts/chore-emojis';
import { ChoreService } from '../../../../core/services/chore/chore.service';
import { KidsDataService } from '../../../../core/services/kids/kids-data.service';
import { Kid } from '../../../../core/types/dtos/kid';
import { ChoreDifficulty } from '../../../../core/types/enums/chore-difficulty';
import { ChoreFrequency } from '../../../../core/types/enums/chore-frequency';
import { LoadingEmoji } from '../../../../shared/components/loading-emoji/loading-emoji';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { EmojiPicker } from '../../components/emoji-picker/emoji-picker';
import { KidAssign } from '../../components/kid-assign/kid-assign';
import { DAYS_OF_WEEK } from '../../config/days-of-week';
import { DifficultyOptions } from '../../config/difficulty-options';
import { FrequencyOptions } from '../../config/frequency-options';

@Component({
  selector: 'app-add-chore',
  imports: [ReactiveFormsModule, AsyncPipe, LoadingScreen, LoadingEmoji, EmojiPicker, KidAssign],
  templateUrl: './add-chore.html',
  styleUrl: './add-chore.scss',
})
export class AddChore {
  private choreService = inject(ChoreService);
  private kidsDataService = inject(KidsDataService);
  private fb = inject(FormBuilder);

  loading = signal(false);
  error = signal<string | null>(null);

  choreDifficultyOptions = DifficultyOptions;
  choreEmojis = CHORE_EMOJIS;
  daysOfWeek = DAYS_OF_WEEK;
  choreFrequencyOptions = FrequencyOptions;
  choreFrequency = ChoreFrequency;

  vm$!: Observable<{
    kids: Kid[];
  }>;

  form = this.fb.nonNullable.group({
    name: ['', { validators: [Validators.required] }],
    icon: ['', { validators: [Validators.required] }],
    kidId: [0, { validators: [Validators.required] }],
    frequency: [ChoreFrequency.Daily, { validators: [Validators.required] }],
    difficulty: [ChoreDifficulty.Easy, { validators: [Validators.required] }],
    dueDay: [null],
    points: [0, { validators: [Validators.required, Validators.min(0)] }],
    description: [''],
  });

  kids$ = this.kidsDataService.kids$;

  ngOnInit() {
    this.loadKids();
  }

  loadKids() {
    this.vm$ = this.kidsDataService.getKids$().pipe(
      map((kids) => {
        if (kids.length && !this.form.value.kidId) {
          this.form.patchValue({ kidId: kids[0].id });
        }

        return { kids };
      }),
    );
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
  }
}
