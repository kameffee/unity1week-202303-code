using MessagePipe;
using Unity1week202303.Actors;
using Unity1week202303.Actors.FemaleBird;
using Unity1week202303.Characters;
using Unity1week202303.Debugging;
using Unity1week202303.InGame;
using Unity1week202303.InGame.Combos;
using Unity1week202303.InGame.Inputs;
using Unity1week202303.InGame.PreCount;
using Unity1week202303.InGame.Ready;
using Unity1week202303.InGame.Result.Failed;
using Unity1week202303.InGame.Result.Success;
using Unity1week202303.InGame.Retry;
using Unity1week202303.InGame.Score;
using Unity1week202303.InGame.Situations;
using Unity1week202303.InGame.State;
using Unity1week202303.InGame.TypingProgress;
using Unity1week202303.InGame.TypingTasks;
using Unity1week202303.Sentences;
using Unity1week202303.Sentences.Data;
using Unity1week202303.TypingProgress;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202303.Installer
{
    public class InGameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private Transform _frontCanvasRoot;

        [SerializeField]
        private GameReadyView _gameReadyViewPrefab;

        [SerializeField]
        private PreCountView _preCountViewPrefab;

        [SerializeField]
        private ResultSuccessView _resultSuccessViewPrefab;

        [SerializeField]
        private ResultFailedView _resultFailedViewPrefab;

        [Header("Debug")]
        [SerializeField]
        private DebugSettings _debugSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_debugSettings);

            // Character
            builder.Register<CharacterTable>(Lifetime.Scoped);
            builder.Register<CharacterTranslator>(Lifetime.Scoped);
            builder.Register<SentenceTranslator>(Lifetime.Scoped);

            // Sentence
            builder.Register<SentenceDataStore>(Lifetime.Scoped);
            builder.Register<SentenceDataTranslator>(Lifetime.Scoped);
            builder.Register<SentenceRepository>(Lifetime.Scoped);

            // Actor
            builder.RegisterComponentInHierarchy<ActorView>();
            builder.RegisterEntryPoint<ActorPresenter>();

            // Female Bird
            builder.RegisterComponentInHierarchy<FemaleBirdView>();
            builder.Register<FemaleBirdPresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();

            // Input
            builder.Register<ForceInputFieldModeUseCase>(Lifetime.Scoped);
            builder.Register<PlayerKeyboardInput>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<PlayerInputPresenter>();
            builder.RegisterComponentInHierarchy<InputFieldView>();

            // Typing
            builder.Register<TypingTaskUseCase>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<TypingProgressView>();
            builder.RegisterEntryPoint<TypingProgressPresenter>();
            builder.Register<TypingProgressService>(Lifetime.Scoped);
            builder.Register<GetTypingResultUseCase>(Lifetime.Scoped);

            // Time
            builder.Register<TimeCounter>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
            builder.Register<RemainingTimer>(Lifetime.Scoped);
            builder.RegisterEntryPoint<GameTimerPresenter>();
            builder.RegisterEntryPoint<RemainingTimePresenter>();
            builder.RegisterComponentInHierarchy<GameTimerView>();

            // Combo
            builder.Register<ComboCounter>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<ComboCountView>();
            builder.RegisterEntryPoint<ComboCountPresenter>();

            var options = builder.RegisterMessagePipe();

            // Sentence
            builder.RegisterComponentInHierarchy<SentenceDisplayView>();
            builder.RegisterEntryPoint<SentenceDisplayPresenter>();

            // GameState
            builder.Register<InGameStateUseCase>(Lifetime.Scoped);

            // Ready
            builder.RegisterFactory<GameReadyView>(() => Instantiate(_gameReadyViewPrefab, _frontCanvasRoot));
            builder.Register<GameReadyPresenter>(Lifetime.Scoped);

            builder.Register<RetryUseCase>(Lifetime.Scoped);

            // PreCount
            builder.Register<PreCountPresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.RegisterFactory<PreCountView>(() => Instantiate(_preCountViewPrefab, _frontCanvasRoot));

            // Situation
            builder.Register<Situation>(Lifetime.Scoped);

            // Result
            // Success
            builder.RegisterMessageBroker<SuccessData>(options);
            builder.Register<GameReultSuccessUseCase>(Lifetime.Scoped);
            builder.RegisterFactory<ResultSuccessView>(() => Instantiate(_resultSuccessViewPrefab, _frontCanvasRoot));
            builder.RegisterEntryPoint<ResultSuccessPresenter>();

            // Failed
            builder.RegisterMessageBroker<FailedData>(options);
            builder.Register<GameResultFailedUseCase>(Lifetime.Scoped);
            builder.RegisterFactory<ResultFailedView>(() => Instantiate(_resultFailedViewPrefab, _frontCanvasRoot));
            builder.RegisterEntryPoint<ResultFailedPresenter>();

            // Score
            builder.Register<ScoreCulculateUseCase>(Lifetime.Scoped);
            builder.Register<ScoreCulculator>(Lifetime.Scoped);

            builder.RegisterComponentInHierarchy<FieldHeartParticle>();

            // Loop
            builder.RegisterEntryPoint<InGameLoop>();

            builder.RegisterBuildCallback(resolver => { resolver.Inject(resolver.Resolve<ActorView>()); });
        }
    }
}
