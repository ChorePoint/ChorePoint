import { KidIdentity } from '../../types/dtos/kid/kid-identity';

export const DEFAULT_KID_IDENTITY = Object.freeze<KidIdentity>({
  name: '',
  age: -1,
  avatar: '',
  lifetimePoints: -1,
  spendablePoints: -1,
  dayStreak: -1,
});
